CREATE TABLE details (
  ID int PRIMARY KEY,
  Name varchar(255) DEFAULT NULL,
  DOB date DEFAULT NULL,
  Father_name varchar(55) DEFAULT NULL,
  Place_of_Birth varchar(55) DEFAULT NULL,
  Nationality varchar(55) DEFAULT NULL,
  Occupation varchar(55) DEFAULT NULL,
  Designation varchar(55) DEFAULT NULL,
  Organisation_Name varchar(55) DEFAULT NULL,
  Organisation_Address varchar(55) DEFAULT NULL,
  Telephone varchar(20) DEFAULT NULL,
  Fax varchar(20) DEFAULT NULL,
  Mobile varchar(20) DEFAULT NULL,
  Permanent_address varchar(255) DEFAULT NULL,
  Present_address varchar(255) DEFAULT NULL,
  P_Telephone varchar(20) DEFAULT NULL,
  P_Mobile varchar(20) DEFAULT NULL,
  P_Fax varchar(20) DEFAULT NULL,
  Place_of_stay varchar(255) DEFAULT NULL,
  Passport_No varchar(20) DEFAULT NULL,
  Place_of_issue varchar(55) DEFAULT NULL,
  Passport_Validity date DEFAULT NULL,
  Visa_No varchar(20) DEFAULT NULL,
  Visa_Place_of_issue varchar(55) DEFAULT NULL,
  Visa_Validity date DEFAULT NULL,
  Visa_type varchar(55) DEFAULT NULL,
  Other_country varchar(255) DEFAULT NULL,
  From_Date date DEFAULT NULL,
  To_Date date DEFAULT NULL
) 