Feature: OpenWeatherMap API Testing
As a weather application user
I want to get current weather data
So that I can make informed decisions

    Background:
        Given I have access to the OpenWeatherMap API

    Scenario: Successful weather request
        Given I have a valid API key
        When I request weather for <city>
        Then I should get a successful response
        And the response should contain weather data
        And the city name should be <expected_city>

        Examples:
          | city      | expected_city |
          | London    | London        |
          | São Paulo | São Paulo     |

    Scenario Outline: Request with invalid, unknown, or empty city
        Given I have a <api_key_type> API key
        When I request weather for <city>
        Then I should get a <status> response
        And the response should contain error message

        Examples:
          | api_key_type | city               | status       |
          | invalid      | London             | unauthorized |
          | valid        | NonExistentCity123 | not found    |
          | valid        |                    | bad request  |
          | valid        | !@#$%^&*()         | not found    |
          | valid        | 123456             | not found    |

    Scenario Outline: Get weather by coordinates
        Given I have a valid API key
        When I request weather with coordinates lat <latitude> and lon <longitude>
        Then I should get a <status> response
        And the response should contain <data> 

        Examples:
          | latitude | longitude | status      | data          |
          | 51.5074  | -0.1278   | successful  | weather data  |
          | 90       | 180       | successful  | weather data  |
          | 91       | 0         | bad request | error message |
          | -91      | 0         | bad request | error message |
          | 0        | 181       | bad request | error message |
          | abc      | 10        | bad request | error message |

    Scenario Outline: Get weather with different units
        Given I have a valid API key
        When I request the weather for <city> in <units> units
        Then I should get a successful response
        And the response should contain weather data
        And the temperature should be in the correct unit for <units>

        Examples:
          | city      | units    |
          | London    | metric   |
          | Paris     | imperial |
          | Tokyo     | standard |
          | Rome      | xxx      |
          | São Paulo |          |