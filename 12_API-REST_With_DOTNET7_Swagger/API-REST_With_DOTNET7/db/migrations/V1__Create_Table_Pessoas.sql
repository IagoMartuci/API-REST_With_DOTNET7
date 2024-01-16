CREATE TABLE `pessoas` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(25) NOT NULL,
  `sobrenome` varchar(50) NOT NULL,
  `endereco` varchar(100) NOT NULL,
  `sexo` varchar(9) NOT NULL,
  `idade` varchar(3) NOT NULL,
  PRIMARY KEY (`id`)
)