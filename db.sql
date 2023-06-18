-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema resiver_shop
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema resiver_shop
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `resiver_shop` DEFAULT CHARACTER SET utf8 COLLATE utf8_bin ;
USE `resiver_shop` ;

-- -----------------------------------------------------
-- Table `resiver_shop`.`customer`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `resiver_shop`.`customer` (
  `idcustomer` INT NOT NULL AUTO_INCREMENT,
  `familia` VARCHAR(45) NOT NULL,
  `imya` VARCHAR(45) NOT NULL,
  `adress` VARCHAR(45) NOT NULL,
  `phone` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`idcustomer`),
  UNIQUE INDEX `imya` (`imya` ASC, `familia` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 6
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_bin;


-- -----------------------------------------------------
-- Table `resiver_shop`.`orders`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `resiver_shop`.`orders` (
  `idorder` INT NOT NULL AUTO_INCREMENT,
  `idcustomer` INT NOT NULL,
  `idresiver` INT NOT NULL,
  `orderdate` DATE NOT NULL,
  `tip_rabot` ENUM('установка', 'ремонт') NOT NULL DEFAULT 'установка',
  `stoimost` DECIMAL(10,0) NOT NULL,
  PRIMARY KEY (`idorder`),
  INDEX `fk_orders_customer1_idx` (`idcustomer` ASC) VISIBLE,
  CONSTRAINT `fk_orders_customer1`
    FOREIGN KEY (`idcustomer`)
    REFERENCES `resiver_shop`.`customer` (`idcustomer`))
ENGINE = InnoDB
AUTO_INCREMENT = 9
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_bin;


-- -----------------------------------------------------
-- Table `resiver_shop`.`interface`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `resiver_shop`.`interface` (
  `idinterface` INT NOT NULL AUTO_INCREMENT,
  `nazvanie` VARCHAR(20) NOT NULL,
  `opisanie` VARCHAR(255) NOT NULL,
  `skorost_peredachi` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`idinterface`))
ENGINE = InnoDB
AUTO_INCREMENT = 5
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_bin;


-- -----------------------------------------------------
-- Table `resiver_shop`.`resiver`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `resiver_shop`.`resiver` (
  `idresiver` INT NOT NULL AUTO_INCREMENT,
  `model` VARCHAR(45) NOT NULL,
  `proizvod` VARCHAR(45) NOT NULL,
  `razreshenie` VARCHAR(45) NOT NULL,
  `idinterface` INT NOT NULL,
  `cena` DECIMAL(10,0) NOT NULL,
  PRIMARY KEY (`idresiver`),
  INDEX `fk_resiver_interface_idx` (`idinterface` ASC) VISIBLE,
  CONSTRAINT `fk_resiver_interface`
    FOREIGN KEY (`idinterface`)
    REFERENCES `resiver_shop`.`interface` (`idinterface`))
ENGINE = InnoDB
AUTO_INCREMENT = 5
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_bin;


-- -----------------------------------------------------
-- Table `resiver_shop`.`composition`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `resiver_shop`.`composition` (
  `idresiver` INT NOT NULL AUTO_INCREMENT,
  `idorder` INT NOT NULL,
  `number` INT NOT NULL,
  PRIMARY KEY (`idresiver`, `idorder`),
  INDEX `fk_composition_orders1_idx` (`idorder` ASC) VISIBLE,
  CONSTRAINT `fk_composition_orders1`
    FOREIGN KEY (`idorder`)
    REFERENCES `resiver_shop`.`orders` (`idorder`),
  CONSTRAINT `fk_composition_resiver1`
    FOREIGN KEY (`idresiver`)
    REFERENCES `resiver_shop`.`resiver` (`idresiver`))
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_bin;


-- -----------------------------------------------------
-- Table `resiver_shop`.`delete_customer`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `resiver_shop`.`delete_customer` (
  `idcustomer` INT NOT NULL AUTO_INCREMENT,
  `familia` VARCHAR(45) NOT NULL,
  `imya` VARCHAR(45) NOT NULL,
  `adress` VARCHAR(45) NOT NULL,
  `phone` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`idcustomer`),
  UNIQUE INDEX `imya` (`imya` ASC, `familia` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 3
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_bin;

USE `resiver_shop` ;

-- -----------------------------------------------------
-- Placeholder table for view `resiver_shop`.`customer_view`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `resiver_shop`.`customer_view` (`idcustomer` INT, `familia` INT, `imya` INT, `adress` INT, `phone` INT);

-- -----------------------------------------------------
-- Placeholder table for view `resiver_shop`.`interface_view`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `resiver_shop`.`interface_view` (`idinterface` INT, `rca-audio` INT, `rca-video` INT);

-- -----------------------------------------------------
-- Placeholder table for view `resiver_shop`.`resiver_view`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `resiver_shop`.`resiver_view` (`idresiver` INT, `model` INT, `an_diap` INT, `dig_diap` INT, `proizvod` INT, `razreshenie` INT, `idinterface` INT, `cena` INT);

-- -----------------------------------------------------
-- function calcul_stoim
-- -----------------------------------------------------

DELIMITER $$
USE `resiver_shop`$$
CREATE DEFINER=`root`@`localhost` FUNCTION `calcul_stoim`(idres int) RETURNS decimal(10,0)
    READS SQL DATA
    DETERMINISTIC
BEGIN
declare c decimal;
select cena into c from resiver where idresiver=idres;
set c=c+500;
RETURN c;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure search_resiver
-- -----------------------------------------------------

DELIMITER $$
USE `resiver_shop`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `search_resiver`(idres int)
    DETERMINISTIC
BEGIN
select * from resiver where idresiver=idres;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- View `resiver_shop`.`customer_view`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `resiver_shop`.`customer_view`;
USE `resiver_shop`;
CREATE  OR REPLACE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `resiver_shop`.`customer_view` AS select `resiver_shop`.`customer`.`idcustomer` AS `idcustomer`,`resiver_shop`.`customer`.`familia` AS `familia`,`resiver_shop`.`customer`.`imya` AS `imya`,`resiver_shop`.`customer`.`adress` AS `adress`,`resiver_shop`.`customer`.`phone` AS `phone` from `resiver_shop`.`customer`;

-- -----------------------------------------------------
-- View `resiver_shop`.`interface_view`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `resiver_shop`.`interface_view`;
USE `resiver_shop`;
CREATE  OR REPLACE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `resiver_shop`.`interface_view` AS select `resiver_shop`.`interface`.`idinterface` AS `idinterface`,`resiver_shop`.`interface`.`rca-audio` AS `rca-audio`,`resiver_shop`.`interface`.`rca-video` AS `rca-video` from `resiver_shop`.`interface`;

-- -----------------------------------------------------
-- View `resiver_shop`.`resiver_view`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `resiver_shop`.`resiver_view`;
USE `resiver_shop`;
CREATE  OR REPLACE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `resiver_shop`.`resiver_view` AS select `resiver_shop`.`resiver`.`idresiver` AS `idresiver`,`resiver_shop`.`resiver`.`model` AS `model`,`resiver_shop`.`resiver`.`an_diap` AS `an_diap`,`resiver_shop`.`resiver`.`dig_diap` AS `dig_diap`,`resiver_shop`.`resiver`.`proizvod` AS `proizvod`,`resiver_shop`.`resiver`.`razreshenie` AS `razreshenie`,`resiver_shop`.`resiver`.`idinterface` AS `idinterface`,`resiver_shop`.`resiver`.`cena` AS `cena` from `resiver_shop`.`resiver`;
USE `resiver_shop`;

DELIMITER $$
USE `resiver_shop`$$
CREATE
DEFINER=`root`@`localhost`
TRIGGER `resiver_shop`.`delete_customer`
BEFORE DELETE ON `resiver_shop`.`customer`
FOR EACH ROW
begin
insert into delete_customer(familia,imya,adress,phone)
values(old.familia,old.imya,old.adress,old.phone);
end$$


DELIMITER ;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
