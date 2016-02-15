use Sales

create view vClientVIP
as
  select Id, Name, VIP
  from Client
  where
    VIP = 1
