DROP TABLE IF EXISTS `bed`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;


DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `role` (
  `RoleID` int NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(255) NOT NULL,
  PRIMARY KEY (`RoleID`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;


DROP TABLE IF EXISTS `employee`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employee` (
  `EmployeeID` int NOT NULL AUTO_INCREMENT,
  `EmployeeSurname` varchar(255) NOT NULL,
  `EmployeeName` varchar(255) NOT NULL,
  `EmployeePatronymic` varchar(255) NOT NULL,
  `Post` varchar(255) NOT NULL,
  `Login` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `Role` int NOT NULL,
  `Photo` mediumblob,
  PRIMARY KEY (`EmployeeID`),
  KEY `aa_idx` (`Role`),
  CONSTRAINT `aa` FOREIGN KEY (`Role`) REFERENCES `role` (`RoleID`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

DROP TABLE IF EXISTS `department`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `department` (
  `DepartmentID` int NOT NULL AUTO_INCREMENT,
  `DepartmentName` varchar(255) NOT NULL,
  `SupervisiorID` int NOT NULL,
  PRIMARY KEY (`DepartmentID`),
  KEY `department_idf_1_idx` (`SupervisiorID`),
  CONSTRAINT `eeee` FOREIGN KEY (`SupervisiorID`) REFERENCES `employee` (`EmployeeID`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

DROP TABLE IF EXISTS `ward`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ward` (
  `WardID` int NOT NULL AUTO_INCREMENT,
  `DepartmentID` int NOT NULL,
  `TypeOfWard` enum('Общая','ВИП','Специализированная') NOT NULL,
  PRIMARY KEY (`WardID`),
  KEY `ee_idx` (`DepartmentID`),
  CONSTRAINT `ee` FOREIGN KEY (`DepartmentID`) REFERENCES `department` (`DepartmentID`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

CREATE TABLE `bed` (
  `BedID` int NOT NULL AUTO_INCREMENT,
  `BedStatus` enum('Свободна','Занята','Санитарная обработка') NOT NULL,
  `WardID` int NOT NULL,
  PRIMARY KEY (`BedID`),
  KEY `eee_idx` (`WardID`),
  CONSTRAINT `eee` FOREIGN KEY (`WardID`) REFERENCES `ward` (`WardID`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;



DROP TABLE IF EXISTS `medicalhistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `medicalhistory` (
  `MedicalHistoryID` int NOT NULL,
  `Diagnosis` varchar(255) NOT NULL,
  `Description` text NOT NULL,
  `DateOfDiagnosis` date NOT NULL,
  PRIMARY KEY (`MedicalHistoryID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

DROP TABLE IF EXISTS `patient`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patient` (
  `PatientID` int NOT NULL AUTO_INCREMENT,
  `PatientSurname` varchar(255) NOT NULL,
  `PatientName` varchar(255) NOT NULL,
  `PatientPatronymic` varchar(255) NOT NULL,
  `Birthday` date NOT NULL,
  `Address` varchar(255) NOT NULL,
  `PhoneNumber` varchar(20) NOT NULL,
  `Insurance_Policy` varchar(255) NOT NULL,
  `MedicalHistoryID` int DEFAULT NULL,
  PRIMARY KEY (`PatientID`),
  KEY `fk_patient_medicalhistory1_idx` (`MedicalHistoryID`),
  CONSTRAINT `fk_patient_medicalhistory1` FOREIGN KEY (`MedicalHistoryID`) REFERENCES `medicalhistory` (`MedicalHistoryID`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

DROP TABLE IF EXISTS `hospitalization`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hospitalization` (
  `HospitalizationID` int NOT NULL AUTO_INCREMENT,
  `PatientID` int NOT NULL,
  `DateOfReceipt` date NOT NULL,
  `DateOfDischarge` date NOT NULL,
  `DepartmentID` int NOT NULL,
  `EmployeeID` int NOT NULL,
  `BedID` int NOT NULL,
  PRIMARY KEY (`HospitalizationID`),
  KEY `hospitalization_ibfk_1` (`PatientID`),
  KEY `hospitalization_ibfk_3` (`EmployeeID`),
  KEY `hospitalization_ibfk_2_idx` (`DepartmentID`),
  KEY `hospitalization_ibfk_4_idx` (`BedID`),
  CONSTRAINT `hospitalization_ibfk_1` FOREIGN KEY (`PatientID`) REFERENCES `patient` (`PatientID`),
  CONSTRAINT `hospitalization_ibfk_2` FOREIGN KEY (`DepartmentID`) REFERENCES `department` (`DepartmentID`),
  CONSTRAINT `hospitalization_ibfk_3` FOREIGN KEY (`EmployeeID`) REFERENCES `employee` (`EmployeeID`),
  CONSTRAINT `hospitalization_ibfk_4` FOREIGN KEY (`BedID`) REFERENCES `bed` (`BedID`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;




