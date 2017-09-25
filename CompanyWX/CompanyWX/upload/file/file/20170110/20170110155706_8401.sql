/*
SQLyog Ultimate v11.27 (32 bit)
MySQL - 5.1.48-log : Database - qdm154828213_db
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`qdm154828213_db` /*!40100 DEFAULT CHARACTER SET gbk */;

USE `qdm154828213_db`;

/*Table structure for table `handsel` */

DROP TABLE IF EXISTS `handsel`;

CREATE TABLE `handsel` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `userid` int(11) DEFAULT NULL,
  `num` int(11) DEFAULT NULL,
  `gold` int(11) DEFAULT NULL,
  `createtime` int(11) DEFAULT NULL,
  `zbid` int(11) DEFAULT NULL,
  `qinmi` int(11) DEFAULT NULL,
  `sumgold` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=10 DEFAULT CHARSET=gbk;

/*Data for the table `handsel` */

insert  into `handsel`(`id`,`userid`,`num`,`gold`,`createtime`,`zbid`,`qinmi`,`sumgold`) values (4,6,1,1,1483712683,8,1,1),(5,6,1,200,1483712703,8,200,200),(6,6,1,200,1483752941,7,200,200),(7,6,1,100,1483752978,7,100,100),(8,6,1,5,1483752981,7,5,5),(9,6,10,50,1483752987,7,5,500);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
