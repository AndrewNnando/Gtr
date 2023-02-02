# Database Assignment

## Assumptions

* Data will be considered clean. The lack of data integrity (referential, null values etc) is not part of the assignment. For example listing id 10 has no user_id. In order not to under-count the number of vendors in Qn 7,
unknown user_ids will be attributed the value of -1.
* Unless impossible, all users are included in queries (left joins are favoured over inner joins).
* It has been assumed that 	a 'user' is the same as a 'vendor'.
* users.status 1 represents 'inactive'
* users.status 2 represents 'active'
* listings.status 2 represents 'basic'
* listings.status 3 represents 'premium'