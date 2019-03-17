create table status_contracts 
(
   id_contracts         integer                        null,
   id_status            integer                        not null,
   status               varchar(30)                    null,
   constraint PK_STATUS_CONTRACTS primary key clustered (id_status)
);

/*==============================================================*/
/* Index: содержит2_FK                                          */
/*==============================================================*/
create index содержит2_FK on status_contracts (
id_contracts ASC
);