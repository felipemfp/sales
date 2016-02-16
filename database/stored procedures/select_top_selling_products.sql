use Sales

create procedure spSelectTopSellingProducts
	@Length int
as
begin
	declare
		@DateStartOfMonth datetime
	set @DateStartOfMonth = dateadd(month, datediff(month, 0, getdate()), 0)
	select top(@Length) P.Id, P.ManufacturerId, P.Description, P.Stock, P.Price
	from SaleProduct SP
		inner join Sale S on SP.SaleId = S.Id
		right join Product P on SP.ProductId = P.Id
	where S.DateSale >= @DateStartOfMonth
	group by P.Id, P.ManufacturerId, P.Description, P.Stock, P.Price
	order by COUNT(P.Id) desc
end
