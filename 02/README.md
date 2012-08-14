# The Purpose #

This is a demo of how to implement one transaction per request using NHibernate and Autofac.
There are several caveats to keep in mind with such an approach; of which will be highlighted below.

# Development Dependencies #

* IIS/IIS Express
* Microsoft Visual Studio 2010+
* SQL Express (preferably 2008/2012)

# Why the PostLogRequestEvent? #

Autofac dynamically registers an HttpModule at runtime that disposes of all instances that implement
IDisposable. This cleanup is done during the EndRequest event. If the session isn't commited before then,
it'll be closed and there will be no chance to save changes. I was forced to sneak in front of Autofac
by tapping in to the event prior to EndReques in the ASP.NET Application Lifecycle. This is the price you
pay for convenience but I'd say it's well worth the expense.

# Why IIS as Opposed to Casini? #

In practice, using IIS for local development is a very good idea and highly recommended. On the job, we actually use
Casini. In this case, I had no choice due to the fact that  the logic in PreApplicationStart relies on Integrated mode 

# What are the Benefits? #

Besides a noticeable performance boost as highlighted in [ episode 1 ] (http://www.youtube.com/watch?v=gi4hhsazwCw), your repositories will be a lot leaner. This approach
alleviates the burden of session and transaction at the repo level. This is an added bonus as repos will be rid of the same
boilerplate code that opens the session, begins the transaction, disposes of both instances in a try finally block, error logging,
etc. This workflow all takes place at a much higher level and it's consistent for each and every request.
