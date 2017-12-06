# giftswap

## Overview
Simple API for generating a gift swap between a group of people. Exclusion groups allow for excluding certain people from being eligible from each other (spouses for instance might not be eligible to gift for each other)

## Sample JSON POST Input:
```json
[
    {"Name": "Kris", "ExclusionGroup": "Phillips"},
    {"Name": "Bonnie", "ExclusionGroup": "Phillips"},
    {"Name": "Jason", "ExclusionGroup": "Couch"},
    {"Name": "Katie", "ExclusionGroup": "Couch"},
    {"Name": "James", "ExclusionGroup": "Barrett"},
    {"Name": "Allison", "ExclusionGroup": "Barrett"},
    {"Name": "Sarah"}
]
```