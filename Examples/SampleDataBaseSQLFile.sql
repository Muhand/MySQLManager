#Create databse
create database MySQLManager;

#Use the database
use MySQLManager;

#Create a new table and call it "info"
create table info
(
	id INT NOT NULL AUTO_INCREMENT,
	name VARCHAR(30) UNIQUE,
	age INT,
	PRIMARY KEY(id)
);