using MongoDB.Bson.Serialization.Attributes;

namespace Microservices.Discount.Api.Repositories
{
    public class BaseEntity
    {
        [BsonElement("_id")]//mongodb de id alanı _id olarak geçer
        public Guid Id { get; set; }
        //servis yazarlen guid kullanıma sebebi değerler eşşiz olunca db ler birleşince karışmaz int olsa ıkı taraftakı aynı sayılar karışır
        //guidleri biz belirleyeceğiz .DB BERLİRLERSE ID ÇAKIŞMASI OLABİLİR ve yuk biner 




        //özel bir sebebin  yoksa INT ARTAN DEĞERLERLE İLERLEME VE DEĞER ÜRETME İŞLEMİNİ DB YE BIRAKMA 
        //DEĞER RANDOM OLSUN VE BU RANDOM DEĞERİ KENDİN ÜRET VE VERİ TABANINA OYLE GONDER 
        //GUİD KISAMINDA INDEXLEMEYI KOLAYLAŞTIRACAK SNOW FLAKE ALGORİTMALARI KULLANILACAK 
        //PRİMARY KEYE SAHIP OLAN ALANLARIN INDEXLENMESİ İŞLEMİNİ KOLAYLAŞTIRIR
        //snow flakes mass transit uzerınden kullanılacak 
    }
}
