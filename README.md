# FrodX.OrderProcessing

This is a NET Developer (MID) Exercise for FrodX.

Scenario
You are tasked with developing a Worker Service application that periodically fetches and processes
mock order data, stores it in a file, and logs the results.

The project is called FrodX.OrderProcessing. It consists of multiple layers: 
- FrodX.OrderProcessing.EFCore (dbcontext and data models)
- FrodX.OrderProcessing.Infrastructure (business logic - repositories)
- FrodX.OrderProcessing.Worker (Worker and Quartz job implementation)
- FrodX.OrderProcessing.WebAPI (Generating orders)

The layers are created in the spirit of Layered architecture. Concerns are well-separated ;)
Project referemces:
- EFCore => /
- Infrastructure => EFCore
- Worker => Infrastructure
- WebAPI => Infrastructure

I managed to implement almost practically everything, including the bonus tasks. There is a small difference between the application and the instructions though, in which I created my own minimal API that randomly generates orders based on a small amount of hardcoded data.

HOW IT WORKS 

All of the Quartz and DI services are created in Worker Program.cs and are properly injected in the CustomJobFactory. This factory calls the service provider, which passes and returns a specific job called OrdersServiceJob. In the job itself, the service provider creates the necessary services and calls the method in IOrderService, which goes through the whole flow of calling the API and saving the data in the database.
Minimal WebApi works lika a normal .Net application - Uses DI and a method (that generates orders) in the Infrastructure layer upon calling the right web address.

There is no hardcoded data; everything is in the appsettings json. I have left the connection string inside as is, because it is localhost & trusted connection and it does not contain any vital information. If you will restore the database script, the application should theoretically work as is.

FACED ISSUES 

I had a huge problem with understanding how to dependency inject services into a factory and It took me way too much time, before I started scrambling around Github and found an example of how that person solves this problem. I am also positive that in a professional enviroment, there are probably certain good practises in developing applications using Quartz, which I don't have much experience in.
