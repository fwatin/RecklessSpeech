Feature: Get All Dictionaries

Scenario: Get All Dictionaries
	Given two language dictionaries
	When the users gets all the language dictionaries
	Then the result contains these two