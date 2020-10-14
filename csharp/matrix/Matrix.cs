using System;
using System.Collections.Generic;
using System.Linq;

public class Matrix
{
    private int[][] _rows;

    public Matrix(string input) => Parse(input);

    public int Rows => _rows.Length;


    public int Cols => _rows.First().Length;


    public int[] Row(int row) => _rows[row - 1];


    public int[] Column(int col) => _rows.Select(r => r[col - 1]).ToArray();


    private void Parse(string m) =>
        _rows = m.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(r =>
                r.Split(' ').Select(c => int.Parse(c)).ToArray()).ToArray();
}
