using Xunit;

// Dit zorgt ervoor dat xUnit alle tests in dit project één voor één uitvoert.
[assembly: CollectionBehavior(DisableTestParallelization = true)]