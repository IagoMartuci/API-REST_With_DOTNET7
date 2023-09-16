CREATE TABLE `livros` (
  `id` INT(10) AUTO_INCREMENT PRIMARY KEY,
  `autor` longtext NOT NULL,
  `data_lancamento` longtext,
  `preco` decimal(65,2),
  `titulo` longtext NOT NULL
)
