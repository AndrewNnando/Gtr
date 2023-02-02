/*
Assumptions
	Data is considered clean. Possible lack of data integrity (referential, null values etc) is not part of the test.
	only include users with results.
	a 'user' is the same as a 'vendor'.
	users.status 1 represents 'inactive'
	users.status 2 represents 'active'
	listings.status 2 represents 'basic'
	listings.status 3 represents 'premium'
*/

/*
*	1. 	Select users whose id is either 3,2 or 4
*		- Please return at least: all user fields
*/
select 
		* 
from 
		users
where 
		id between 2 and 4;

/*
*	2. 	Count how many basic and premium listings each active user has
*		- Please return at least: first_name, last_name, basic, premium
*/
select 
		u.first_name, u.last_name, 
		Sum(case when l.status = 2 then 1 else 0 end) basic, 
		Sum(case when l.status = 3 then 1 else 0 end) premium
from 
		users u
left join 
		listings l on u.id=l.user_id
where 
		u.status = 2
group by 
		u.first_name, u.last_name ;

/*
*	3. Show the same count as before but only if they have at least ONE premium listing
*		- Please return at least: first_name, last_name, basic, premium
*/

select 
		u.first_name, u.last_name, 
		Sum(case when l.status = 2 then 1 else 0 end) basic, 
		Sum(case when l.status = 3 then 1 else 0 end) premium
from 
		users u
left join 
		listings l on u.id=l.user_id
where 
		u.status = 2		
group by 
		u.first_name, u.last_name 
having
		Sum(case when l.status = 3 then 1 else 0 end) > 0;

/*
*	4. 	How much revenue has each active vendor made in 2013
*		- Please return at least: first_name, last_name, currency, revenue
*/      
select 
		u.first_name, u.last_name , coalesce(c.currency,'-') as currency, coalesce(Sum(c.price),0) as revenue
from 
		users u
left join 
		listings l on u.id=l.user_id
left join
		clicks c on l.id=c.listing_id
where 
		u.status = 2
 and
		year(c.created) = 2013
group by
		u.first_name, u.last_name, c.currency;
      
/*
*	5. 	Insert a new click for listing id 3, at $4.00
*		- Find out the id of this new click. Please return at least: id
*/      
Insert clicks
	(listing_id, price, currency, created)
Values
	(3, 4, 'USD', now());
    
Select
	LAST_INSERT_ID() as id;

/*
*	6.	Show listings that have not received a click in 2013
*		- Please return at least: listing_name
*/   

select 
		l.name as listing_name
from
		listings l
left join
		clicks c 
        on 
			l.id = c.listing_id 
		and 
			year(c.created) = 2013
where
		c.listing_id is null;

/*
*	7. 	For each year show number of listings clicked and number of vendors who owned these listings
*		- Please return at least: date, total_listings_clicked, total_vendors_affected
*
*	Unknown user_ids (null values) will be represented by the user_id -1. This prevents under-counting
*   the vendor count.
*/

select 
		dateyear as date, Sum(total_listings_clicked) as total_listings_clicked, Count(user_id) as total_vendors_affected
From 
	(
		select 
				dateyear, user_id, count(*) as total_listings_clicked
		from
				(
				select distinct 
						year(created) as dateyear, coalesce(user_id,-1) as user_id, listing_id
				from
						clicks c
				left join
						listings l on c.listing_id = l.id
				) x
		group by
				dateyear, user_id
	) x
group by
	dateyear
order by 
		1;

/*
*	8.	Return a comma separated string of listing names for all active vendors
*		- Please return at least: first_name, last_name, listing_names
*/

select
		u.first_name, u.last_name, coalesce(group_concat(l.name separator ','),'') as listing_names
from
		users u
left join
		listings l on u.id=l.user_id
 where
		u.status=2
group by
		u.first_name, u.last_name;
