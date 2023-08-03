using BenchmarkDotNet.Attributes;

namespace MemoryAllocation.Tests;

[MemoryDiagnoser]
public class OffsetVsCursor
{
    private List<int> listPagination = new();
    private int pageSize = 5;

    // Offset
    private int middlePage;
    private int lastPage;

    // Cursor
    private int firstCursor;
    private int middleCursor;
    private int lastCursor;

    [Params(1000, 10000, 100000)]
    public int ListSize;

    [GlobalSetup]
    public void Setup()
    {
        listPagination = Enumerable.Range(1, ListSize).ToList();

        // Offset
        middlePage = (ListSize / pageSize) / 2;
        lastPage = ListSize / pageSize;

        // Cursor
        middleCursor = listPagination.ElementAt(ListSize / 2);
        lastCursor = listPagination.Last();
    }

    [Benchmark]
    public List<int> PaginationOffsetFirstPage()
    {
        return listPagination
           .Skip((1 - 1) * pageSize)
           .Take(pageSize)
           .ToList();
    }

    [Benchmark]
    public List<int> PaginationOffsetMiddlePage()
    {
        return listPagination
           .Skip((middlePage - 1) * pageSize)
           .Take(pageSize)
           .ToList();
    }

    [Benchmark]
    public List<int> PaginationOffsetLastPage()
    {
        return listPagination
           .Skip((lastPage - 1) * pageSize)
           .Take(pageSize)
           .ToList();
    }

    [Benchmark]
    public List<int> PaginationCursorFisrtPage()
    {
        return listPagination
           .Where(i => i >= 1)
           .Take(pageSize)
           .ToList();
    }

    [Benchmark]
    public List<int> PaginationCursorMiddlePage()
    {
        return listPagination
           .Where(i => i >= middleCursor)
           .Take(pageSize)
           .ToList();
    }

    [Benchmark]
    public List<int> PaginationCursorLastPage()
    {
        return listPagination
           .Where(i => i >= lastCursor)
           .Take(pageSize)
           .ToList();
    }
}
