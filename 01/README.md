# The Purpose #

To demonstrate the importance of allowing NHibernate to buffer your changes and commit them in chunks.
In practice, this is known as the [Unit of Work pattern] ( http://en.wikipedia.org/wiki/Database_transaction ).
It's a bit more than just a database transaction. The use of transactions isn't necessarily a UoW. It's more so
about the use of one global/aggregate transaction to consolidate what would normally be 2 or more individal operations
executed against the database.

For example, a UserRepository is saving a user and during the same request, and OrderRepository needs to create an order.
Rather than saving the user and then the order, both entities are persisted as apart of the same transaction.

This demo demonstrates performning 1000 individual writes, versus queuing 1000 writes in proc via SaveChanges and
committing a single transaction. In the case of the latter, NHibernate will perform 50 writes with 20 inserts.
This can be viewed as a 95% performance increase.

# System Requirements #

* Visual Studio 2010+ (Exress should suffice)
* [SQL Express 2012]: (2005/2008 should  work as well but not sure. I used 2012 in the screencast)
    * [32 - bit] ( http://download.microsoft.com/download/8/D/D/8DD7BDBA-CEF7-4D8E-8C16-D9F69527F909/ENU/x86/SQLManagementStudio_x86_ENU.exe )
    * [64 - bit] ( http://download.microsoft.com/download/8/D/D/8DD7BDBA-CEF7-4D8E-8C16-D9F69527F909/ENU/x64/SQLManagementStudio_x64_ENU.exe )
* [NH Profiler] ( http://nhprof.com/ ) (assuming you want to see how NHibernate handles the 2 scnearios in question)
