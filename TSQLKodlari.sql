﻿create database UdemyDoviz
GO
Use UdemyDoviz
GO
create table ParaBirimi
(
ID uniqueidentifier primary key ,
Code nvarchar(8),
Tanim nvarchar(70),
UyariLimit decimal -- 0 2.34   2.33 >  2.34 < 2.43
)
GO

insert into ParaBirimi (ID,Code,Tanim,UyariLimit) values (newid(),'USD','Amerikan Doları',4.25)
insert into ParaBirimi (ID,Code,Tanim,UyariLimit) values (newid(),'EUR','EURO',0)
insert into ParaBirimi (ID,Code,Tanim,UyariLimit) values (newid(),'GBP','İngiliz Sterlini',0)

go 

create table Kur
(
ID uniqueidentifier primary key,
ParaBirimiID uniqueidentifier,
Alis decimal ,
Satis decimal , 
OlusturmaTarih datetime
)

GO 

create table KurGecmis
(
ID uniqueidentifier primary key,
KurID uniqueidentifier,
ParaBirimiID uniqueidentifier,
Alis decimal ,
Satis decimal , 
OlusturmaTarih datetime
)

go 

create proc KurKayitEKLE
(
@ID uniqueidentifier,
@ParaBirimiID uniqueidentifier,
@Alis decimal ,
@Satis decimal , 
@OlusturmaTarih datetime
)
as
begin

if((select count(*) from Kur where ParaBirimiID = @ParaBirimiID)>0)
begin

-- Kur tablosundaki mevcut kaydı bizim kurgecmis tablosuna aktarmamız gerekiyor. 
insert into KurGecmis (ID,KurID,ParaBirimiID,Alis,Satis,OlusturmaTarih) select newid(),ID,ParaBirimiID,Alis,Satis,OlusturmaTarih from Kur where ParaBirimiID = @ParaBirimiID
-- Kur tablosundaki değerimizi güncelleyelim. 

update Kur set 
Alis = @Alis,
Satis = @Satis
where 
ParaBirimiID = @ParaBirimiID


end
else 
begin
insert into Kur (ID,ParaBirimiID,Alis,Satis,OlusturmaTarih) values (@ID,@ParaBirimiID,@Alis,@Satis,@OlusturmaTarih)
end

end