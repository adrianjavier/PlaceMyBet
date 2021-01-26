CREATE SCHEMA IF NOT EXISTS `PlaceMyBet` DEFAULT CHARACTER SET utf8 ;
USE `PlaceMyBet` ;

CREATE TABLE IF NOT EXISTS `PlaceMyBet`.`Evento` (
  `idEvento` INT NOT NULL AUTO_INCREMENT,
  `local` VARCHAR(45) NOT NULL,
  `visitante` VARCHAR(45) NOT NULL,
  `fecha`  VARCHAR(20) NOT NULL,
  PRIMARY KEY (`idEvento`));

CREATE TABLE IF NOT EXISTS `PlaceMyBet`.`Mercado` (
  `idMercado` INT NOT NULL AUTO_INCREMENT,
  `cuotaOver` DOUBLE NOT NULL,
  `cuotaUnder` DOUBLE Not NULL,
  `dineroOver` DOUBLE NOt NULL,
  `dineroUnder` DOUBLE NOT NULL,
  `tipoMercado` DOUBLE NOT NULL,
  `idEvento` INT NOT NULL,
  PRIMARY KEY (`idMercado`),
  CONSTRAINT `relacionEvento` FOREIGN KEY (`idEvento`) REFERENCES `PlaceMyBet`.`Evento` (`idEvento`) ON DELETE NO ACTION ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `PlaceMyBet`.`Usuario` (
  `gmail` VARCHAR(50) NOT NULL,
  `nombre` VARCHAR(45) NOT NULL,
  `apellido` VARCHAR(45) NOT NULL,
  `edad` INT NOT NULL,
  PRIMARY KEY (`gmail`));

CREATE TABLE IF NOT EXISTS `PlaceMyBet`.`Apuesta` (
  `idApuesta` INT NOT NULL AUTO_INCREMENT,
  `tipoMercado` DOUBLE NOT NULL,
  `cuota` DOUBLE NOT NULL,
  `dinero` DOUBLE NOT NULL,
  `fecha` VARCHAR(40) NOT NULL,
  `idMercado` INT NOT NULL,
  `gmail` VARCHAR(50) NOT NULL,
  `tipoCuota` VARCHAR(10) NOT NULL,
  PRIMARY KEY (`idApuesta`),
  CONSTRAINT `relacionUsuario` FOREIGN KEY (`gmail`) REFERENCES `PlaceMyBet`.`Usuario` (`gmail`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `relacionMercado` FOREIGN KEY (`idMercado`) REFERENCES `PlaceMyBet`.`Mercado` (`idMercado`) ON DELETE NO ACTION ON UPDATE NO ACTION);

CREATE TABLE IF NOT EXISTS `PlaceMyBet`.`Cuenta Bancaria` (
  `gmail` VARCHAR(50) NOT NULL,
  `saldo` DECIMAL(10) NOT NULL,
  `nombreBanco` VARCHAR(45) NOT NULL,
  `numTarjeta` INT NOT NULL,
  PRIMARY KEY (`gmail`),
  CONSTRAINT `relacionUsuario2` FOREIGN KEY (`gmail`) REFERENCES `PlaceMyBet`.`Usuario` (`gmail`) ON DELETE NO ACTION ON UPDATE NO ACTION);


INSERT INTO `PlaceMyBet`.`Evento` (`idEvento`, `local`, `visitante`, `fecha`) VALUES (1, 'Valencia', 'Espanyol', '2020-09-20');
INSERT INTO `PlaceMyBet`.`Evento` (`idEvento`, `local`, `visitante`, `fecha`) VALUES (2, 'Barcelona', 'Madrid', '2020-09-19');
INSERT INTO `PlaceMyBet`.`Mercado` (`idMercado`, `cuotaOver`, `cuotaUnder`, `dineroOver`, `dineroUnder`, `tipoMercado`, `idEvento`) VALUES (1, 1.45, 2.85, 50, 100, 2.5, 1);
INSERT INTO `PlaceMyBet`.`Mercado` (`idMercado`, `cuotaOver`, `cuotaUnder`, `dineroOver`, `dineroUnder`, `tipoMercado`, `idEvento`) VALUES (2, 2.8, 1.2, 100, 50, 2.5, 1);
INSERT INTO `PlaceMyBet`.`Mercado` (`idMercado`, `cuotaOver`, `cuotaUnder`, `dineroOver`, `dineroUnder`, `tipoMercado`, `idEvento`) VALUES (3, 2.78, 1.5, 100, 100, 3.5, 2);
INSERT INTO `PlaceMyBet`.`Usuario` (`gmail`, `nombre`, `apellido`, `edad`) VALUES ('adbaro@gmail.com', 'Adrian', 'Ballesteros', 20);
INSERT INTO `PlaceMyBet`.`Usuario` (`gmail`, `nombre`, `apellido`, `edad`) VALUES ('sami@gmail.com', 'Sami', 'Martinez', 23);
INSERT INTO `PlaceMyBet`.`Apuesta` (`idApuesta`, `tipoMercado`, `cuota`, `dinero`, `fecha`, `idMercado`, `gmail`, `tipoCuota`) VALUES (1, 2.5, 1.89, 100, '2020-09-18', 1, 'adbaro@gmail.com', 'over');
INSERT INTO `PlaceMyBet`.`Apuesta` (`idApuesta`, `tipoMercado`, `cuota`, `dinero`, `fecha`, `idMercado`, `gmail`, `tipoCuota`) VALUES (2, 3.5, 1.3, 200, '2020-09-18', 3, 'sami@gmail.com', 'under');
INSERT INTO `PlaceMyBet`.`Cuenta Bancaria` (`gmail`, `saldo`, `nombreBanco`, `numTarjeta`) VALUES ('adbaro@gmail.com', 500, 'Bankia', 123456789);
INSERT INTO `PlaceMyBet`.`Cuenta Bancaria` (`gmail`, `saldo`, `nombreBanco`, `numTarjeta`) VALUES ('sami@gmail.com', 900, 'Santander', 987654321);