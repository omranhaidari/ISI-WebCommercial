-- phpMyAdmin SQL Dump
-- version 4.5.0.2
-- http://www.phpmyadmin.net
--
-- Client :  127.0.0.1
-- Généré le :  Mer 18 Novembre 2015 à 09:48
-- Version du serveur :  10.0.17-MariaDB
-- Version de PHP :  5.6.14

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `commercial`
--

-- --------------------------------------------------------

--
-- Structure de la table `articles`
--

create database commercial;
use commercial;
CREATE TABLE `articles` (
  `NO_ARTICLE` char(6) NOT NULL,
  `LIB_ARTICLE` char(30) NOT NULL,
  `QTE_DISPO` int(11) NOT NULL,
  `VILLE_ART` char(15) NOT NULL,
  `PRIX_ART` decimal(8,2) NOT NULL,
  `INTERROMPU` char(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `articles`
--

INSERT INTO `articles` (`NO_ARTICLE`, `LIB_ARTICLE`, `QTE_DISPO`, `VILLE_ART`, `PRIX_ART`, `INTERROMPU`) VALUES
('001001', 'Bureau Electronique', 2, 'Nantes', '674.03', 'F'),
('001002', 'Ensemble bureau (Secretaire)', 2, 'Paris', '868.43', 'F'),
('001005', 'Ensemble bureau (Directeur)', 1, 'Nantes', '1880.53', 'F'),
('001007', 'Table en bois - pied central', 29, 'Lyon', '753.47', 'F'),
('001008', 'Meuble micro-ordinateur', 22, 'Paris', '629.62', 'F'),
('001009', 'Chaise - dossier reglable', 124, 'Nantes', '87.66', 'T'),
('001013', 'Chaise (Moderne) pneumatique', 115, 'Nantes', '80.60', 'F'),
('001015', 'Porte coulissante ebene', 15, 'Lyon', '1215.00', 'T'),
('001019', 'Table de reunion', 12, 'Lyon', '750.00', 'F'),
('001021', 'Ensemble bureau (President)', 3, 'Lyon', '1965.53', 'F'),
('001022', 'Table en orme', 5, 'Lyon', '604.65', 'F'),
('001024', 'Table en aluminium', 140, 'Nantes', '295.53', 'F'),
('001025', 'Bureau (Directeur) - 2 metres', 63, 'Paris', '395.00', 'F'),
('001027', 'Bureau (Directeur) - 3 metres', 20, 'Nantes', '225.00', 'F'),
('001029', 'Armoire - 2 tiroirs', 200, 'Lyon', '130.65', 'F'),
('001031', 'Chaise (Directeur) - pivotante', 79, 'Paris', '84.00', 'F'),
('001032', 'Armoire - 4 tiroirs', 15, 'Nantes', '242.83', 'F'),
('001033', 'Chaise - accoudoirs standard', 20, 'Nantes', '375.00', 'T'),
('001038', 'Lampe - bras articule', 169, 'Paris', '47.13', 'F');

-- --------------------------------------------------------

--
-- Structure de la table `clientel`
--

CREATE TABLE `clientel` (
  `NO_CLIENT` char(6) NOT NULL,
  `SOCIETE` char(25) NOT NULL,
  `NOM_CL` char(15) NOT NULL,
  `PRENOM_CL` char(10) NOT NULL,
  `ADRESSE_CL` char(20) NOT NULL,
  `VILLE_CL` char(15) NOT NULL,
  `CODE_POST_CL` char(5) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `clientel`
--

INSERT INTO `clientel` (`NO_CLIENT`, `SOCIETE`, `NOM_CL`, `PRENOM_CL`, `ADRESSE_CL`, `VILLE_CL`, `CODE_POST_CL`) VALUES
('000001', 'Design Leonard', 'Adami', 'Lucien', '24, rue Saint Lo', 'Vernon', '27200'),
('000003', 'Meubles Langlais', 'Ladelis', 'Laurent', '42, rue Josephine', 'Angers', '49000'),
('000009', 'Aux Meubles sur Mesures', 'Ralain', 'Gerard', 'Place Carnaval', 'Mornant', '69440'),
('000011', 'Mobilier Americain', 'Bojolait', 'Christine', '28, rue Chopin', 'Nimes', '13100'),
('000016', 'Fournitures et Bureaux', 'Guilleaume', 'Jerome', '20, rue Borville', 'Elbeuf', '76500'),
('000017', 'Le Mobilier Noir', 'Camelot', 'Jean', '46, rue Maillot', 'Chevrieres', '60710'),
('000018', 'Systemes Interieurs', 'Gonzalez', 'Bruno', '17, rue Curie', 'Loiret', '45000'),
('000019', 'Au Grand Design', 'Ancelot', 'Jerome', '7, place Hitchcock', 'Bandol', '83150'),
('000022', 'Mobilier de Paris', 'Marbella', 'Max', '17, place Rabelais', 'Amiens', '80000'),
('000024', 'Bois et Metal', 'Farre', 'Philippe', '59, rue Sambat', 'Gisors', '27140'),
('000025', 'Le Mobilier Moderne', 'Vasille', 'Isabelle', '47, rue des Pres', 'Chantilly', '60500'),
('000027', 'Agencement de Bureaux', 'Ravard', 'Jacques', '65, boulevard Jasmin', 'Ermenonville', '60950'),
('000028', 'Conception et Bois', 'Pavent', 'Raymond', '12, place Fontaine', 'Cabourg', '14390'),
('000031', 'Le Mobilier de la vallee', 'Rasazin', 'Jacky', '2, route de l arbre', 'Blain', '44130'),
('000032', 'Design Contemporain', 'Jeaumont', 'Dominique', '6, rue Emile Zola', 'Evron', '53600'),
('000033', 'La Maison Interieure', 'Scheinder', 'Marc', '31, rue Saturnin', 'Fos-sur-mer', '13270'),
('000034', 'Le Bureau Japonais', 'Quelavoine', 'Charles', '36, avenue Frank', 'Blonville', '14910'),
('000035', 'Le Mobilier de la Vallee', 'Hellamy', 'Andre', '40, rue Federation', 'Orleans', '45000'),
('000036', 'Nouveaux Horizons', 'Zelinger', 'Jacques', '4, rue Georges Sand', 'Nantes', '44000'),
('000040', 'Espaces de Vie', 'Penoist', 'Alain', '5, rue Rolland', 'Jumieges', '76118'),
('000042', 'Meubles Cohen', 'Pujo', 'Bernard', '52, rue Pannette', 'Evreux', '27000'),
('000043', 'Les meubles d Antan', 'Berenger', 'Michel', '20, rue Alouette', 'Arles', '30100'),
('000045', 'Les Classiques', 'Ternard', 'Pierre', '40, rue Ferrari', 'Guingamp', '22200'),
('000046', 'Interieurs de bureaux', 'Lerthaux', 'Alain', '12, avenue d Alleray', 'Bourges', '18000');

-- --------------------------------------------------------

--
-- Structure de la table `commandes`
--

CREATE TABLE `commandes` (
  `NO_COMMAND` char(6) NOT NULL,
  `NO_VENDEUR` char(6) NOT NULL,
  `NO_CLIENT` char(6) NOT NULL,
  `DATE_CDE` date NOT NULL,
  `FACTURE` char(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `commandes`
--

INSERT INTO `commandes` (`NO_COMMAND`, `NO_VENDEUR`, `NO_CLIENT`, `DATE_CDE`, `FACTURE`) VALUES
('020002', '000008', '000025', '2015-06-19', 'F'),
('020003', '000006', '000043', '2015-06-20', 'F'),
('020004', '000001', '000034', '2015-05-21', 'F'),
('020005', '000001', '000016', '2015-08-21', 'F'),
('020006', '000012', '000036', '2015-10-23', 'F'),
('020007', '000013', '000019', '2015-09-23', 'F'),
('020008', '000003', '000011', '2015-09-21', 'F'),
('020009', '000012', '000018', '2015-11-20', 'F'),
('020010', '000001', '000031', '2015-07-18', 'F'),
('020011', '000020', '000040', '2015-08-29', 'F'),
('020012', '000008', '000027', '2015-09-24', 'F'),
('020013', '000012', '000036', '2015-06-26', 'F'),
('020014', '000001', '000001', '2015-06-23', 'F'),
('020015', '000004', '000019', '2015-07-24', 'F'),
('020016', '000013', '000011', '2015-09-18', 'F'),
('020017', '000006', '000032', '2015-09-25', 'F'),
('020018', '000013', '000036', '2015-09-28', 'F'),
('020019', '000013', '000016', '2015-09-29', 'F'),
('020020', '000013', '000031', '2015-09-18', 'F'),
('020021', '000008', '000046', '2015-08-14', 'F'),
('020022', '000004', '000027', '2015-09-17', 'F'),
('020023', '000003', '000040', '2015-10-22', 'F'),
('020024', '000012', '000045', '2015-10-01', 'F'),
('020025', '000003', '000019', '2015-10-05', 'F'),
('020026', '000004', '000017', '2015-10-07', 'F');

-- --------------------------------------------------------

--
-- Structure de la table `compose`
--

CREATE TABLE `compose` (
  `NO_COMPOSE` char(6) NOT NULL,
  `NO_COMPOSANT` char(6) NOT NULL,
  `QTE_COMPOSANT` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `compose`
--

INSERT INTO `compose` (`NO_COMPOSE`, `NO_COMPOSANT`, `QTE_COMPOSANT`) VALUES
('001001', '001007', 1),
('001001', '001013', 1),
('001001', '001032', 1),
('001001', '001038', 1),
('001002', '001013', 1),
('001002', '001025', 1),
('001002', '001032', 1),
('001005', '001024', 1),
('001005', '001027', 1),
('001005', '001031', 1),
('001021', '001015', 1),
('001021', '001024', 1),
('001021', '001025', 1),
('001021', '001031', 1);

-- --------------------------------------------------------

--
-- Structure de la table `detail_cde`
--

CREATE TABLE `detail_cde` (
  `NO_COMMAND` char(6) NOT NULL,
  `NO_ARTICLE` char(6) NOT NULL,
  `QTE_CDEE` int(11) DEFAULT NULL,
  `LIVREE` char(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `detail_cde`
--

INSERT INTO `detail_cde` (`NO_COMMAND`, `NO_ARTICLE`, `QTE_CDEE`, `LIVREE`) VALUES
('020002', '001013', 3, 'F'),
('020002', '001025', 3, 'F'),
('020002', '001032', 2, 'F'),
('020003', '001005', 2, 'F'),
('020003', '001021', 4, 'F'),
('020004', '001013', 5, 'F'),
('020004', '001027', 5, 'F'),
('020004', '001038', 5, 'F'),
('020005', '001019', 2, 'F'),
('020006', '001007', 25, 'F'),
('020006', '001031', 25, 'F'),
('020007', '001022', 3, 'F'),
('020007', '001033', 3, 'F'),
('020008', '001009', 3, 'F'),
('020009', '001029', 31, 'F'),
('020010', '001005', 5, 'F'),
('020010', '001021', 2, 'F'),
('020011', '001025', 4, 'F'),
('020011', '001029', 7, 'F'),
('020011', '001031', 7, 'F'),
('020012', '001015', 5, 'F'),
('020013', '001019', 1, 'F'),
('020013', '001022', 2, 'F'),
('020014', '001021', 2, 'F'),
('020015', '001025', 15, 'F'),
('020016', '001025', 2, 'F'),
('020016', '001031', 4, 'F'),
('020017', '001029', 6, 'F'),
('020018', '001038', 4, 'F'),
('020019', '001027', 3, 'F'),
('020020', '001024', 7, 'F'),
('020020', '001032', 4, 'F'),
('020021', '001013', 8, 'F'),
('020021', '001024', 6, 'F'),
('020021', '001025', 8, 'F'),
('020022', '001015', 1, 'F'),
('020023', '001024', 12, 'F'),
('020023', '001032', 2, 'F'),
('020024', '001009', 3, 'F'),
('020024', '001027', 3, 'F'),
('020025', '001019', 1, 'F'),
('020026', '001007', 9, 'F'),
('020026', '001013', 9, 'F'),
('020026', '001015', 3, 'F'),
('020026', '001024', 5, 'F'),
('020026', '001025', 3, 'F');

-- --------------------------------------------------------

--
-- Structure de la table `vendeur`
--

CREATE TABLE `vendeur` (
  `NO_VENDEUR` char(6) NOT NULL,
  `NO_VEND_CHEF_EQ` char(6) NOT NULL,
  `NOM_VEND` char(15) NOT NULL,
  `PRENOM_VEND` char(10) NOT NULL,
  `DATE_EMBAU` date NOT NULL,
  `VILLE_VEND` char(15) NOT NULL,
  `SALAIRE_VEND` decimal(8,2) NOT NULL,
  `COMMISSION` decimal(6,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `vendeur`
--

INSERT INTO `vendeur` (`NO_VENDEUR`, `NO_VEND_CHEF_EQ`, `NOM_VEND`, `PRENOM_VEND`, `DATE_EMBAU`, `VILLE_VEND`, `SALAIRE_VEND`, `COMMISSION`) VALUES
('000001', '000001', 'Zimmerman', 'Alphonse', '2000-12-02', 'Paris', '3500.00', '50.00'),
('000003', '000001', 'Vidoni', 'Lise', '2000-04-06', 'Lyon', '2280.00', '50.00'),
('000004', '000001', 'Coudray', 'Bruno', '2000-03-05', 'Paris', '2500.00', '50.00'),
('000006', '000001', 'Thomas', 'Pierre', '2001-05-23', 'Lyon', '2856.00', '50.00'),
('000008', '000001', 'Mauleron', 'Arianne', '2003-08-27', 'Paris', '1300.00', '50.00'),
('000011', '000001', 'Gorbach', 'Michel', '2000-05-29', 'Nantes', '1960.00', '70.00'),
('000012', '000004', 'Charles', 'Edouard', '2002-06-23', 'Nantes', '1600.00', '50.00'),
('000013', '000004', 'Marin', 'Jean', '2000-07-18', 'Paris', '1500.00', '11.00'),
('000015', '000004', 'Rodinck', 'Auguste', '2000-09-15', 'Lyon', '1700.00', '80.00'),
('000016', '000004', 'Long', 'Gerard', '2000-07-19', 'Lyon', '1700.00', '80.00'),
('000019', '000004', 'Rolfes', 'Patrick', '2001-02-25', 'Paris', '1600.00', '60.00'),
('000020', '000001', 'Sandrin', 'Alex', '2000-04-29', 'Nantes', '1730.00', '50.00');

--
-- Index pour les tables exportées
--

--
-- Index pour la table `articles`
--
ALTER TABLE `articles`
  ADD PRIMARY KEY (`NO_ARTICLE`);

--
-- Index pour la table `clientel`
--
ALTER TABLE `clientel`
  ADD PRIMARY KEY (`NO_CLIENT`);

--
-- Index pour la table `commandes`
--
ALTER TABLE `commandes`
  ADD PRIMARY KEY (`NO_COMMAND`),
  ADD KEY `fk1_commandes` (`NO_VENDEUR`),
  ADD KEY `fk2_commandes` (`NO_CLIENT`);

--
-- Index pour la table `compose`
--
ALTER TABLE `compose`
  ADD PRIMARY KEY (`NO_COMPOSE`,`NO_COMPOSANT`),
  ADD KEY `fk2_compose` (`NO_COMPOSANT`);

--
-- Index pour la table `detail_cde`
--
ALTER TABLE `detail_cde`
  ADD PRIMARY KEY (`NO_COMMAND`,`NO_ARTICLE`),
  ADD KEY `fk2_detail_cde` (`NO_ARTICLE`);

--
-- Index pour la table `vendeur`
--
ALTER TABLE `vendeur`
  ADD PRIMARY KEY (`NO_VENDEUR`),
  ADD KEY `fk1_vendeur` (`NO_VEND_CHEF_EQ`);

--
-- Contraintes pour les tables exportées
--

--
-- Contraintes pour la table `commandes`
--
ALTER TABLE `commandes`
  ADD CONSTRAINT `fk1_commandes` FOREIGN KEY (`NO_VENDEUR`) REFERENCES `vendeur` (`NO_VENDEUR`),
  ADD CONSTRAINT `fk2_commandes` FOREIGN KEY (`NO_CLIENT`) REFERENCES `clientel` (`NO_CLIENT`);

--
-- Contraintes pour la table `compose`
--
ALTER TABLE `compose`
  ADD CONSTRAINT `fk1_compose` FOREIGN KEY (`NO_COMPOSE`) REFERENCES `articles` (`NO_ARTICLE`),
  ADD CONSTRAINT `fk2_compose` FOREIGN KEY (`NO_COMPOSANT`) REFERENCES `articles` (`NO_ARTICLE`);

--
-- Contraintes pour la table `detail_cde`
--
ALTER TABLE `detail_cde`
  ADD CONSTRAINT `fk1_detail_cde` FOREIGN KEY (`NO_COMMAND`) REFERENCES `commandes` (`NO_COMMAND`),
  ADD CONSTRAINT `fk2_detail_cde` FOREIGN KEY (`NO_ARTICLE`) REFERENCES `articles` (`NO_ARTICLE`);

--
-- Contraintes pour la table `vendeur`
--
ALTER TABLE `vendeur`
  ADD CONSTRAINT `fk1_vendeur` FOREIGN KEY (`NO_VEND_CHEF_EQ`) REFERENCES `vendeur` (`NO_VENDEUR`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
