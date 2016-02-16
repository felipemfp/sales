# Sales - Sistema de Venda

**Instituição**: Instituto Federal do Rio Grande do Norte

**Curso**: Técnico em Informática para Internet

**Período**: 4º

**Disciplina**: Programação Orientada a Serviços

**Professor**: Gilbert Azevedo

**Arquivo**: [20151_pos_lista11.pdf](http://diatinf.ifrn.edu.br/antigo/lib/exe/fetch.php?media=corpodocente:gilbert:20151_pos_lista11.pdf)

## Objetivos

Desenvolver aplicações com a utilização de serviços e programação em bancos de dados. 

## Funcionalidades e Requisitos de Casos de Uso

- [ ] Crud Fabricante, Crud Produto e Crud Cliente devem ser realizados utilizando LINQ acessando as respectivas tabelas no banco de dados. 

- [x] Cadastro  de  Venda  deve  ser  realizado  com  procedimentos  armazenados  no  banco  de  dados  para  inserir  os dados referentes a uma venda (dados da venda e itens vendidos) nas respectivas tabelas do banco. O desconto dado em uma venda deve ser x% sobre o total dos itens, onde x é o número de vendas já realizadas ao cliente nos últimos seis meses, limitado a 10%. Para clientes VIPs, o desconto é fixado em 15%, independente do  número  de  vendas  anteriores.  Os  valores  totais  com  e  sem  desconto  e  o  valor  do  desconto  devem  ser armazenados no banco. 

- [x] Cliente  VIP  deve  ser  realizado  utilizando  procedimento  armazenado  e  gatilho.  Um  cliente  passa  a  ser  VIP quando tiver pelos menos 20 vendas nos últimos 12 meses e perde esse status caso passe mais de 90 dias sem comprar. Esse caso de uso deve ser disparado sempre que uma venda for cadastrada. 

- [x] Atualizar Estoque deve ser realizado utilizando procedimento armazenado e gatilho. Ele deve ser utilizado para alterar o estoque dos itens cadastrados na venda e deve gerar uma exceção caso o estoque de um produto fique negativo. 

- [x] Listagem  de  Vendas  deve  ser  realizada  através  de  procedimentos  armazenados  que  retorne  a  relação  de vendas em um determinado período ou de um determinado cliente. 

- [x] Listagem  de  Produtos  Mais  Vendidos  no  Mês  deve  ser  realizada  com  um  procedimento  armazenado  que retorne a relação com os x produtos mais vendidos em um mês, onde x é um número informado pelo usuário. 

- [x] Listagem de Clientes VIPs deve utilizar uma visão para retornar a relação de clientes VIPs. 
