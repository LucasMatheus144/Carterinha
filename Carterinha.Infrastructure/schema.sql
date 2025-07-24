create sequence public.carterinha_seq;

create table public.carteira(
	id integer primary key default nextval('public.carterinha_seq'),
    nome varchar(25) not null unique,
    instituicao varchar(25) not null,
    cpf varchar(14) not null unique,
    rg varchar(12) not null unique,
    d_nascimento date not null,
    d_validade date not null    
);