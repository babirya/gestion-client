create database gestionclientdb 

use gestionclientdb 

create table ville (codeville int primary key , nomville varchar(20) )

create  table client (codeclient int primary key , nomclient varchar(20) , prenomclient varchar(20) ,
 codeville int foreign  key references ville(codeville), datenaissance date ) 

 select  * from ville  
 select * from client  
 -- insertion  into ville
 insert into ville values(1,'casablanca') 
 insert into ville values(2,'rabat') 
 insert into ville values(3,'beni mellal') 
 insert into ville values(4,'tadla') 
 insert into ville values(5,'tangier') 
 insert into ville values(6,'dakhla')
 insert into ville values(7,'agadir') 
 insert into ville values(8,'marracheck') 
 insert into ville values(9,'berchid')  

  -- insertion  into client 
  insert into client values (1,'massat','abdessamad',2,'12-03-2000')
  insert into client values (2,'mobarik','tarik',1,'12-02-2001')
  insert into client values (3,'khadoum','walid',3,'08-04-2000')
  insert into client values (4,'bakhouch','morad',4,'04-02-2000')
  insert into client values (5,'othmane','fahd',5,'11-05-2001')
  insert into client values (6,'samad','oussama',6,'10-06-2001')
  insert into client values (7,'dbar','mouhsine',7,'10-10-2000')
  insert into client values (8,'anguiry','intan',8,'12-03-1999')
  insert into client values (9,'kachach','mohamed',9,'12-03-1995')

  select codeclient, nomclient , prenomclient, nomville,datenaissance 
  from  client inner join ville on client.codeville = ville.codeville 
  

 
 select count (codeclient) from client where codeclient = 1
