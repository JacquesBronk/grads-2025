# End 2 End Testing for RetroShop

## How to run the tests
1. Change the directory to the root of the project
2. Run the following command:
```bash
dotnet test tests/E2ERetroShop
```

## How to write new tests
1. Create a new class in the `tests/E2ERetroShop` directory
2. Inherit from the `BaseTest` class
3. Write in Given-When-Then style

## How does scoring work?
The scoring is based on the number of tests that pass. The more tests that pass, the higher the score.

* You will earn 1 point for each test that passes
* New features & tests 5 points
* New services & tests 5 points

## How to submit your tests
1. Create a new branch with the following
   2. YourName
3. Push your branch to the repository
4. Create a pull request to the `main` branch
5. Automatically, the tests will run and the score will be calculated

