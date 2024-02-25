--[MS SQL]
--#Connect Master
IF exists (SELECT name FROM master.dbo.sysdatabases WHERE name = '%DB')
   DROP DATABASE [%DB]
------------
IF exists (SELECT name FROM sysusers WHERE name = '%USER')
   DROP USER [%USER];
------------
IF exists (SELECT name FROM syslogins WHERE name = '%USER')
   DROP LOGIN [%USER];
------------
IF not exists (SELECT name FROM master.dbo.sysdatabases WHERE name = '%DB')
   CREATE DATABASE [%DB];
------------
CREATE LOGIN [%USER] WITH password = '%PASSWORD',
      default_database = [%DB],
      check_policy = OFF,
      check_expiration = OFF;
------------
USE %DB;
------------
EXEC sp_grantdbaccess '%USER';
------------
EXEC sp_addrolemember 'db_owner', '%USER';

--[Oracle]
--#Connect Master
DROP USER %USER CASCADE;
--------------
DROP TABLESPACE %USER INCLUDING CONTENTS AND DATAFILES;
--------------
CREATE SMALLFILE TABLESPACE %USER DATAFILE '%USER.DBF'
   SIZE 10M LOGGING EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO DEFAULT NOCOMPRESS;
--------------
CREATE USER %USER IDENTIFIED BY %PASSWORD DEFAULT TABLESPACE %USER TEMPORARY TABLESPACE Temp;
------------
GRANT UNLIMITED TABLESPACE TO %USER
------------
GRANT CONNECT, RESOURCE TO %USER
------------
ALTER USER %USER DEFAULT ROLE "CONNECT","RESOURCE";
------------
ALTER USER %USER QUOTA UNLIMITED ON %USER;
------------
GRANT create session TO %USER
------------
GRANT create sequence TO %USER
------------
GRANT create synonym TO %USER
------------
GRANT create table TO %USER
------------
GRANT create procedure TO %USER
--#Connect %DSN
CREATE TABLE "Language" (
   lang_id int NOT NULL,
   Name char(12) NOT NULL)
------------
CREATE SYNONYM LANGUAGE FOR "Language"
------------
CREATE SEQUENCE Lang_Seq MINVALUE 1 NOCACHE
------------
CREATE OR REPLACE TRIGGER Lang_Trg BEFORE INSERT ON Language
FOR EACH ROW
   DECLARE tmpVar NUMBER;
   BEGIN
      tmpVar := 0;
      SELECT Lang_Seq.NEXTVAL INTO tmpVar FROM dual;
      :NEW.lang_id := tmpVar;
   END;
