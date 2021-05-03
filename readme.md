We are going to run thru 5 different scenarios where you can replace if-else or switch cases with polymorphism using objects.

There are a bunch of reasons why you'd want to stop using the 'else' keyword.

If-Else is the defacto standard for code branching, which makes sense because it's easy and often
the very first thing any developer learns.

Using If-Else everywhere gets nasty quickly. Especially whenever you need to extend or add new functionality.



**Scene: Accounts**
Our first example deals with states. Sometimes you want to an object to behave differently depending on its state.

Here we have our demo class. A very basic Account. 
For our example, an account can be in one of three states. open, closed, or, frozen.

Also, an account may allow overdraft.

Take a look at the WithdrawMoney method. The branching is not too bad. I've seen way worse in real production code.
But, branching like this is not ideal for two simple reasons:
1) It an be difficult to spot bugs, especially with deep nesting
2) Adding new functionality is a pain, and even more so if it includes making new branches or states

We can eliminate these branches by encapsulating each state in its own object.
This is obviously more code, but, looking at each object in isolation, you can see how easy it is to debug, extend functionality and add new states.


