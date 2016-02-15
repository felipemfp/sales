use Sales

create procedure spSelectTopSellingProducts
	@Length int
as
begin
	select top(@Length) P.Id, P.ManufacturerId, P.Description, P.Stock, P.Price
	from SaleProduct SP
		right join Product P on SP.ProductId = P.Id
	group by P.Id, P.ManufacturerId, P.Description, P.Stock, P.Price
	order by COUNT(P.Id) desc
end
