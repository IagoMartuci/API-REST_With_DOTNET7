CREATE TABLE `livros` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `autor` longtext NOT NULL,
  `titulo` longtext NOT NULL,
  `preco` decimal(65,2),
  `id_usuario` INT NOT NULL,
  `nome_usuario` longtext NOT NULL,
  `data_cadastro` longtext,
  `id_usuario_alteracao` INT,
  `nome_usuario_alteracao` longtext,
  `data_alteracao` longtext
)
