Feature: Import new sequences

    Scenario: Happy Path - import some new sequences
        Given a file containing some sequences
        When the user imports this file
        Then some sequences are saved

    Scenario: The imported sequence is valid HTML
        Given a file containing some sequences
        When the user imports this file
        Then the html in HTML Content is valid

    Scenario: The HTMl in the imported sequence has a title and the images
        Given a file containing some sequences
        When the user imports this file
        Then the HTML contains some nodes for title and images
        
        