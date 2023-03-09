Feature: Send Notes to Anki

    Scenario: Send some note to Anki
        Given an existing sequence
        When the user sends the sequence to Anki
        Then a corresponding note is sent to Anki

    Scenario: The dutch note sent to Anki contains explanation in the AFTER field
        Given a dutch sequence
        Given some existing explanation for this dutch word
        When the user sends the sequence to Anki
        Then the anki note contains the translation for the word in the after field

    Scenario: The dutch note sent to Anki contains source in the source field
        Given a dutch sequence
        Given some existing explanation for this dutch word
        When the user sends the sequence to Anki
        Then the anki note contains the source