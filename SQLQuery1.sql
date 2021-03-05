select Sellers.Name, Sellers.Surname, Quant = (select CONCAT(SUM(Sales.Quantity * Products.Price), ' p.') as 'Продажи'
from Sellers, Sales, Products
where Sellers.Id = Sales.IDSel
and Products.ID = Sales.IDProd
and Sellers.ID = 2003)
from Sellers, Sales, Products
where Sellers.Id = Sales.IDSel
and Products.ID = Sales.IDProd

select Sellers.Name
from Sellers

select CONCAT(SUM(Sales.Quantity * Products.Price), ' p.') as 'Продажи'
from Sellers, Sales, Products
where Sellers.Id = Sales.IDSel
and Products.ID = Sales.IDProd
and Sellers.ID = 2003


select Sellers.Name, Sellers.Surname, Sales.Quantity, Products.Price, Mone = (Sales.Quantity * Products.Price)
from Sellers, Sales, Products
where Sellers.Id = Sales.IDSel
and Products.ID = Sales.IDProd

            --//var clientsRequest = BankDB.Clients.SelectMany(
            --//    client => BankDB.Persons.Where(person => client.idClient == person.idClient).DefaultIfEmpty(),
            --//    (client, person) => new
            --//    {
            --//        client.idClient,
            --//        client.isPerson,
            --//        client.clientName,
            --//        client.adress,
            --//        client.account,
            --//        client.invoice,
            --//        person.cName,
            --//        person.isVip
            --//    })
            --//    .SelectMany(
            --//    client => BankDB.Entity.Where(entity => client.idClient == entity.idClient).DefaultIfEmpty(),
            --//    (client, entity) => new
            --//    {
            --//        client.idClient,
            --//        client.isPerson,
            --//        client.clientName,
            --//        client.adress,
            --//        client.account,
            --//        client.invoice,
            --//        client.cName,
            --//        client.isVip,
            --//        entity.dirName,
            --//        entity.dirSurname
            --//    });
            --//foreach (var i in clientsRequest)
            --//{
            --//    if (i.isPerson)
            --//    {
            --//        DBClients.Add(new Person(i.cName, (decimal)i.invoice, (decimal)i.earning, i.idClient, i.clientName, i.adress, i.isVip == true));
            --//    }
            --//    else
            --//    {
            --//        DBClients.Add(new MainLibrary.Clients.Entity(i.clientName, i.dirName, i.dirSurname, (decimal)i.invoice, (decimal)i.earning, i.idClient, i.adress));
            --//    }
            --//}
            --//var personRequest = BankDB.Clients.Join(
            --//    BankDB.Persons,
            --//    client => client.idClient,
            --//    person => person.idClient,
            --//    (client, person) => new
            --//    {
            --//        client.idClient,
            --//        client.clientName,
            --//        client.adress,
            --//        client.invoice,
            --//        client.earning,
            --//        person.cName,
            --//        person.isVip
            --//    });