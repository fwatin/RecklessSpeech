Feature: Import sequences from zip

    Scenario: Happy Path - import some new sequences
        Given a zip file containing two sequences
        When the user imports this zip file
        Then two sequences are saved