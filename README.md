#Umbraco Test Framework

**NOTE: This is still in development.**

A test framework for Umbraco 7 that abstracts Umbraco functionality into interfaces that can be tested. This is designed for a pure MVC implementation of Umbraco with strongly-typed *Models*, thin *Controllers*, and finally *Views*.

It wraps content retrieval behind an interface called INodeService that in turn uses testable interfaces for UmbracoHelper, and IPublishedContent extension methods (like Children and Ancestors).

Andy Butland's [UmbracoMapper](https://github.com/AndyButland/UmbracoMapper) is used to map IPublishedContent to strongly-typed models which can be used in the views.

The dependency injection is handled using Castle.Windsor.

Detailed documentation can be found in the [wiki](https://github.com/shanebru/UmbracoTestFramework/wiki).

This framework is intended for new Umbraco installations as it's the base framework for interacting with Umbraco.