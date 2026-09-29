namespace JuegoElementos.ConsoleApp.Models;

/// <summary>
/// A style for a box.
/// </summary>
public record struct BoxStyle
{
    public char TopLeftCorner { get; }
    public char TopRightCorner { get; }
    public char BottomLeftCorner { get; }
    public char BottomRightCorner { get; }

    public char HorizontalLineTop { get; }
    public char HorizontalLineBottom { get; }
    public char VerticalLineLeft { get; }
    public char VerticalLineRight { get; }

    /// <summary>
    /// Creates a new box style.
    /// </summary>
    /// <param name="topLeftCorner">The top-left corner of the box.</param>
    /// <param name="topRightCorner">The top-right corner of the box.</param>
    /// <param name="bottomLeftCorner">The bottom-left corner of the box.</param>
    /// <param name="bottomRightCorner">The bottom-right corner of the box.</param>
    /// <param name="horizontalLine">The horizontal line of the box. This will be used for both top and bottom lines.</param>
    /// <param name="verticalLine">The vertical line of the box. This will be used for both left and right lines.</param>
    public BoxStyle(
        char topLeftCorner,
        char topRightCorner,
        char bottomLeftCorner,
        char bottomRightCorner,
        char horizontalLine,
        char verticalLine)
    {
        TopLeftCorner = topLeftCorner;
        TopRightCorner = topRightCorner;
        BottomLeftCorner = bottomLeftCorner;
        BottomRightCorner = bottomRightCorner;
        HorizontalLineTop = HorizontalLineBottom = horizontalLine;
        VerticalLineLeft = VerticalLineRight = verticalLine;
    }

    /// <summary>
    /// Creates a new box style.
    /// </summary>
    /// <param name="topLeftCorner">The top-left corner of the box.</param>
    /// <param name="topRightCorner">The top-right corner of the box.</param>
    /// <param name="bottomLeftCorner">The bottom-left corner of the box.</param>
    /// <param name="bottomRightCorner">The bottom-right corner of the box.</param>
    /// <param name="horizontalLineTop">The top horizontal line of the box.</param>
    /// <param name="horizontalLineBottom">The bottom horizontal line of the box.</param>
    /// <param name="verticalLineLeft">The left vertical line of the box.</param>
    /// <param name="verticalLineRight">The right vertical line of the box.</param>
    public BoxStyle(
        char topLeftCorner,
        char topRightCorner,
        char bottomLeftCorner,
        char bottomRightCorner,
        char horizontalLineTop,
        char horizontalLineBottom,
        char verticalLineLeft,
        char verticalLineRight)
    {
        TopLeftCorner = topLeftCorner;
        TopRightCorner = topRightCorner;
        BottomLeftCorner = bottomLeftCorner;
        BottomRightCorner = bottomRightCorner;
        HorizontalLineTop = horizontalLineTop;
        HorizontalLineBottom = horizontalLineBottom;
        VerticalLineLeft = verticalLineLeft;
        VerticalLineRight = verticalLineRight;
    }

    /// <summary>
    /// The default box style.
    /// <br />
    /// It uses the `─│┌┐└┘` characters to draw the box.
    /// </summary>
    public static readonly BoxStyle Default = new(
        topLeftCorner: '┌',
        topRightCorner: '┐',
        bottomLeftCorner: '└',
        bottomRightCorner: '┘',
        horizontalLine: '─',
        verticalLine: '│'
    );
}
