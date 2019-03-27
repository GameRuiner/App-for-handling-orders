# App-for-handling-orders
Po włączeniu wpisujemy pliki ze ścieżką do nic. Z zaproponowanych raportów wybieramy potrzebny odpowiednii.
Pliki wpisujemy, rozdzielając spacją. Przykładowo:

D:\Users\MarkG\source\repos\test1.csv D:\Users\MarkG\source\repos\test2.xml D:\Users\MarkG\source\repos\test3.json

Dla wyboru raportu lub sposobu sortowania wybieramy odpowiednią literą.

Każdy plik zawiera jedno lub więcej zamówień

### XML
```xml
<requests>
<request>
<clientId>3</clientId>
<requestId>1</requestId>
<name>Ser</name>
<quantity>2</quantity>
<price>13.00</price>
</request>
<request>
<clientId>3</clientId>
<requestId>2</requestId>
<name>Chleb</name>
<quantity>2</quantity>
<price>15.00</price>
</request>
</requests>
```

### JSON
```json
{
"requests":[
{
"clientId":"5",
"requestId":"1",
"name":"Bułka",
"quantity":"1",
"price":"10.00"
}
]
}
```
### CSV
```csv
Client_Id,Request_id,Name,Quantity,Price
1,1,Bułka,1,10.00
1,2,Chleb,2,15.00
1,2,Chleb,5,15.00
2,1,Chleb,1,10.00
```
