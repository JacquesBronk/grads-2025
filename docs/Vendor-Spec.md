# Vendor Service Specification

## Purpose

Our shop has become one of the premiere shops to purchase any retro games, however our stock is something to left desired. After countless meetings and hours, we’ve built relationships with multiple vendors that could sell products on our platform. 

However, we must maintain the flexibility in these relationships to allow our administrators to add & remove vendors when appropriate. This provides vendors with the opportunity to add their own stock to our collection for a percentage of profit going towards us.

## Scope

We need an API for third party vendors to manage stock within the retro shop. The vendors must be created by our administrative team and at any given time vendors can be created, updated, deleted or removed.

The stock API needs to be updated so that it accepts a vendor Id and be flagged to show this is a third-party product. We want our customers to be well informed. 

### Requirement

The following will explain the business & technical requirements for the vendors API

#### Vendor Data Requirements

The vendor should cater for the following fields so that we can evaluate and effectively manage third party vendors.
1.	Vendor name.
2.	Vendor address.
3.	Vendor status could be, Active, Inactive, Vetting.
4.	Vendor order lead time, this is how many days the order would take to reach our warehouse.
5.	Vendor payment information so we can pay our vendors.
6.	Vendor payment history so we can determine what we’ve paid to each vendor.
7.	Vendor contact information, note there may be multiple contact with the same vendor.
8.	Vendor Sales, we want to determine which vendor is most profitable. 

#### Vendor Expected Endpoints

1.	POST: http://vendor-api/vendors/create 
2.	GET: http://vendor-api/vendors/{Id}
3.	PUT: http://vendor-api/vendors/{Id}/update
4.	DELETE: http://vendor-api/vendors/{Id}/delete
5.	PUT: http://vendor-api/vendors/{Id}/updateStatus/{Status}
6.	POST: http://vendor-api/vendors/{Id}/sales
7.	GET: http://vendor-api/vendors/{Id}/sales/total
8.	GET: http://vendor-api/vendors/{Id}/sales/from/{date}
9.	GET: http://vendor-api/vendors/{Id}/sales/from/{date}/to/{date}
10.	GET: http://vendor-api/vendors/{Id}/contacts
11.	GET: http://vendor-api/vendors/{Id}/contacts/getDefault
12.	POST: http://vendor-api/vendors/{Id}/createContact
13.	PUT: http://vendor-api/vendors/{Id}/updateContact
14.	DELETE: http://vendor-api/vendors/{Id}/deleteContact/{contactId}
15.	GET: http://vendor-api/vendors/sales/total
16.	GET: http://vendor-api/vendors/sales/total/top10
17.	GET: http://vendor-api/vendors/sales/total/top50
18.	GET: http://vendor-api/vendors/sales/total/top/{n}
19.	GET: http://vendor-api/vendors/sales/total/for/lastmonth
20.	GET: http://vendor-api/vendors/sales/total/for/lastweek
21.	GET: http://vendor-api/vendors/sales/total/for/yesterday
22.	GET: http://vendor-api/vendors/sales/average/for/{vendorId}
23.	GET: http://vendor-api/vendors/sales/total/{top/bottom}/{n}

