using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Bussiness.DataProtection;
using VirtualArtGallery.Bussiness.Operations.Artwork.Dtos;
using VirtualArtGalleryProject.Data.Entities;
using VirtualArtGalleryProject.Data.Repository;
using VirtualArtGalleryProject.Data.UnitOfWork;

namespace VirtualArtGallery.Bussiness.Operations.Artwork
{
    public class ArtworkManager : IArtworkService
    {
        private readonly IRepository<ArtworkEntity> _artworkRepository;
        private readonly IRepository<ExhibitionArtworkEntity> _exhibitionArtworkRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArtworkManager(IRepository<ArtworkEntity> artworkRepository,IUnitOfWork unitOfWork, IRepository<ExhibitionArtworkEntity> exhibitionArtworkRepository)
        {
            _unitOfWork = unitOfWork;
            _artworkRepository = artworkRepository;
            _exhibitionArtworkRepository = exhibitionArtworkRepository;
        }

        public  async Task<ServiceMessage> AddArtwork(ArtworkDto artworkDto)//Yeni sanat eseri ekleme metotu
        {
            var artworkControl= _artworkRepository.GetAll(x=>x.Name.ToLower()==artworkDto.Name.ToLower()).Any();
            if (artworkControl)//Daha önce veri tabanına bu isimde veri girişi olmuşmun onu kontrol ediyoruz
            {//Eğer veri girişi var ise kullanıcıya service message dönüyorum
                return new ServiceMessage
                {
                    IsSuccseed = false,
                    Message="Bu isimde eser bulunduğu için kayıt yapamıyoruz farklı bir isim deneyiniz"
                };


            }
            

            await _unitOfWork.BeginTransaction();//Ekleme sırasında bir aksaklık olabildeği için transaction kullanıyorum 
            
            var artworkEntity = new ArtworkEntity//ArtworkEntity e ait verilieri aktarıyorum 
            {
                Name = artworkDto.Name,
                Price = artworkDto.Price,
               UserId = artworkDto.UserId,

            };
            _artworkRepository.Add(artworkEntity);//Artwork tablosuna ekleme yapıldı 
            
            try//Ekleme sırasında bir hata oluştu mu kontrol ediyoruz
            {
            await _unitOfWork.SaveChangesAsync();


            }
            catch (Exception)
            {

            throw new Exception("Kayıt sırasında bir hata oluştu");
            }
        
            //Many to many bir tablo olduğu için exhibition tablosuna da ekleme yapıyoruz 
            //Birden çok exhibitiona ait olabileceği içn döngü ile ekliyorum 
           foreach (var ExhibitionId  in artworkDto.ExhibitionIds)
           {
                var exhibiton = new ExhibitionArtworkEntity
                {
                    ExhibitionId= ExhibitionId,
                    ArtworkId=artworkEntity.Id

                };

               _exhibitionArtworkRepository.Add(exhibiton);
           }
           //son kotrol yapılarak kayıt edililiyor hata alınır ise rollback ile yapılan değişiklikler geri alınıyor  
          try
          {
         await _unitOfWork.SaveChangesAsync();
         await _unitOfWork.CommitTransaction();
          }
          catch (Exception)
          {
             await _unitOfWork.RollBackTransaction();
             throw new Exception("Kayıt sırasında hata oluştu");

          }
       

            return new ServiceMessage
            {
                IsSuccseed = true
            };

        }

        public async Task<ServiceMessage> DeleteAretwork(int id)
        {
            var artwork = _artworkRepository.GetById(id);//Id ye sahip sanat eseri var mı kontrol ediliyor
            if (artwork is null)
            {
                return new ServiceMessage
                {
                    IsSuccseed = false,
                    Message = "Silmek istenen kullanıcı bulunamadı "
                };
            }
            _artworkRepository.Delete(id);
            try
            {
                _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Silme sırasında bir hata oluştu ");
            }
            return new ServiceMessage
            {
                IsSuccseed = true
            };
        }

        public async Task<List<ArtworkInfoDto>> GetAllArtwork()
        {//Veri tabanındaki bütün verileri çekiyoruz ve liste halinde controllere gönderiyoruz
            var artworks = await _artworkRepository.GetAll()
                .Select(x => new ArtworkInfoDto
                {
                    Name = x.Name,
                    Price = x.Price,
                    Exhibition = x.ExhibitionArtwork.Select(e => new ArtworkExhibitionDto
                    {
                        Id = e.Id,
                        Date = e.Exhibition.Date,
                        Name = e.Exhibition.Name,
                    }).ToList()

                }).ToListAsync();
            return artworks;
        }


        public async Task<ArtworkInfoDto> GetArtwork(int id)
        {
          var artwork= await  _artworkRepository.GetAll(x => x.Id == id)//Id li veriyi buluyoruz
                 .Select(x => new ArtworkInfoDto
                 {
                     Name = x.Name,
                     Price = x.Price,
                     Exhibition = x.ExhibitionArtwork.Select(e => new ArtworkExhibitionDto
                     {
                         Id = e.Id,
                         Date = e.Exhibition.Date,
                         Name = e.Exhibition.Name,
                     }).ToList()

                 }).FirstOrDefaultAsync();//Yakalanan ilk veriyi dönüyoruz veri bulunmaz ise null dönüyoruz
            return artwork;
        }

        //Price verisinin güncellenmesi
        public async Task<ServiceMessage> Changetwork(int id, decimal priceTo)
        {
            var control = _artworkRepository.GetById(id);
            if (control is null)
            {
                return new ServiceMessage
                {
                    IsSuccseed = false,
                    Message = "Aranan id bulunamadı"
                };
            }
            control.Price = priceTo;
            _artworkRepository.Update(control);
            try
            {
                _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Değişim sırasında bir hata oluştu");
            }
            return new ServiceMessage
            {
                IsSuccseed = true,
            };
        }
        //Kullanıcının gönderdiği idnin bütün verilerini güncelliyoruz
        public async Task<ServiceMessage> UpdateArtwork(UpdateArtworkDto artworkDto)
        {
           var artworkEntity=_artworkRepository.GetById(artworkDto.Id);
            if (artworkEntity is null)
            {
                return new ServiceMessage
                {
                    IsSuccseed = false,
                    Message = "Değişiklik yapılacak id bulunamadı"
                };
            }
            //Kullanıcıdan alınan veriler veri tabanına aktarılıyor
            await _unitOfWork.BeginTransaction();
            artworkEntity.Id= artworkDto.Id;
            artworkEntity.Name=artworkDto.Name;
            artworkEntity.Price=artworkDto.Price;
            artworkEntity.UserId=artworkDto.UserId;

            _artworkRepository.Update(artworkEntity);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Güncelleme sırasında bir hata oluştu ");

            }

            var artworkExhibition = _exhibitionArtworkRepository.GetAll(x => x.ArtworkId == x.ArtworkId).ToList();
            foreach (var artwork in artworkExhibition)
            {
                _exhibitionArtworkRepository.Delete(artwork, false);//Hard delete yapıyoruz çünkü yerine yeni veriler eklelemek için 
            }
            //Kullanıcıdan alına exhibition veri dizisini veri sayısı kadar foreach döngüsü ile ekliyoruz
            foreach (var exhibitionId in artworkDto.ExhibitionIds)
            {
                var artwork = new ExhibitionArtworkEntity
                {
                    ArtworkId = artworkEntity.Id,
                    ExhibitionId = exhibitionId.Id,

                };
                _exhibitionArtworkRepository.Add(artwork);
            }

            try
            {
              await  _unitOfWork.SaveChangesAsync();
              await  _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Kayıt sırasına bir hata oluştu");
            }

            return new ServiceMessage
            {
                IsSuccseed = true,
            };
        }
    }
}
