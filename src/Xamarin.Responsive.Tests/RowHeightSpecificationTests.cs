using FluentAssertions;
using Xamarin.Forms;
using Xunit;

namespace Xamarin.Responsive.Tests
{
    [Collection("WindowSize")]
    public class RowHeightSpecificationTests : BaseTests
    {
        [Fact]
        public void ShouldDefaultToAutoHeight()
        {
            Container container;
            Row row;
            MockView view;

            UseWindowWidth(1200);

            var grid = new Grid
            {
                IsPlatformEnabled = true,
                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }
                },

                Children =
                {
                    (container = new Container
                    {
                        IsPlatformEnabled = true,

                        Children =
                        {
                            (row = new Row
                            {
                                IsPlatformEnabled = true,
                                Children =
                                {
                                    (view = new MockView(20) {
                                        IsPlatformEnabled = true,
                                    })
                                }
                            })
                        }
                    })
                }
            };

            grid.Layout(new Rectangle(Point.Zero, WindowSize));

            container.Bounds.Should().Be(new Rectangle(Point.Zero, WindowSize));
            row.Bounds.Should().Be(new Rectangle(0, 0, 1200, 20));
            view.Bounds.Should().Be(new Rectangle(0, 0, 100, 20));
        }

        [Fact]
        public void ShouldFillHeight()
        {
            Container container;
            Row row;
            MockView view;

            UseWindowWidth(1200);

            var grid = new Grid
            {
                IsPlatformEnabled = true,
                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }
                },

                Children =
                {
                    (container = new Container
                    {
                        IsPlatformEnabled = true,

                        Children =
                        {
                            (row = new Row
                            {
                                IsPlatformEnabled = true,
                                HeightSpecifications = new ResponsiveRowHieghtSpecification(xs: new RowHeightSpecification(1, RowHeightUnit.Fill)),
                                Children =
                                {
                                    (view = new MockView(20) {
                                        IsPlatformEnabled = true,
                                    })
                                }
                            })
                        }
                    })
                }
            };

            grid.Layout(new Rectangle(Point.Zero, WindowSize));

            container.Bounds.Should().Be(new Rectangle(Point.Zero, WindowSize));
            row.Bounds.Should().Be(new Rectangle(0, 0, 1200, container.Height));
            view.Bounds.Should().Be(new Rectangle(0, 0, 100, 20));
        }

        [Fact]
        public void ShouldRowFillsAvailableHeight()
        {
            Container container;
            Row row;
            MockView view;

            UseWindowWidth(1200);

            var grid = new Grid
            {
                IsPlatformEnabled = true,
                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }
                },

                Children =
                {
                    (container = new Container
                    {
                        IsPlatformEnabled = true,

                        Children =
                        {
                            new Row
                            {
                                IsPlatformEnabled = true,
                                Children = {
                                    new MockView(20)
                                }
                            },
                            (row = new Row
                            {
                                IsPlatformEnabled = true,
                                HeightSpecifications = new ResponsiveRowHieghtSpecification(xs: new RowHeightSpecification(1, RowHeightUnit.Fill)),
                                Children =
                                {
                                    (view = new MockView(20) {
                                        IsPlatformEnabled = true,
                                    })
                                }
                            }),
                            new Row
                            {
                                IsPlatformEnabled = true,
                                Children = {
                                    new MockView(20)
                                }
                            },
                        }
                    })
                }
            };

            grid.Layout(new Rectangle(Point.Zero, WindowSize));

            container.Bounds.Should().Be(new Rectangle(Point.Zero, WindowSize));
            row.Bounds.Should().Be(new Rectangle(0, 20, 1200, container.Height - 40));
            view.Bounds.Should().Be(new Rectangle(0, 0, 100, 20));
        }

        [Fact]
        public void ShouldDistributeAvailableHeightToMulitpleFillRowsEqually()
        {
            Container container;
            Row row1;
            Row row2;
            MockView view;

            UseWindowWidth(1200);

            var grid = new Grid
            {
                IsPlatformEnabled = true,
                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }
                },

                Children =
                {
                    (container = new Container
                    {
                        IsPlatformEnabled = true,

                        Children =
                        {
                            new Row
                            {
                                IsPlatformEnabled = true,
                                Children = {
                                    new MockView(20)
                                }
                            },
                            (row1 = new Row
                            {
                                IsPlatformEnabled = true,
                                HeightSpecifications = new ResponsiveRowHieghtSpecification(xs: new RowHeightSpecification(1, RowHeightUnit.Fill)),
                                Children =
                                {
                                    (view = new MockView(20) {
                                        IsPlatformEnabled = true,
                                    })
                                }
                            }),
                            (row2 = new Row
                            {
                                IsPlatformEnabled = true,
                                HeightSpecifications = new ResponsiveRowHieghtSpecification(xs: new RowHeightSpecification(1, RowHeightUnit.Fill)),
                                Children =
                                {
                                    (view = new MockView(20) {
                                        IsPlatformEnabled = true,
                                    })
                                }
                            }),
                            new Row
                            {
                                IsPlatformEnabled = true,
                                Children = {
                                    new MockView(20)
                                }
                            },
                        }
                    })
                }
            };

            grid.Layout(new Rectangle(Point.Zero, WindowSize));

            container.Bounds.Should().Be(new Rectangle(Point.Zero, WindowSize));
            row1.Bounds.Should().Be(new Rectangle(0, 20, 1200, (container.Height - 40) * 0.5));
            row2.Bounds.Should().Be(new Rectangle(0, ((container.Height - 40) * 0.5) + 20, 1200, (container.Height - 40) * 0.5));
            view.Bounds.Should().Be(new Rectangle(0, 0, 100, 20));
        }

        [Fact]
        public void ShouldDistributeAvailableHeightToMulitpleFillRowsProportionally()
        {
            Container container;
            Row row1;
            Row row2;
            MockView view;

            UseWindowWidth(1200);

            var grid = new Grid
            {
                IsPlatformEnabled = true,
                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }
                },

                Children =
                {
                    (container = new Container
                    {
                        IsPlatformEnabled = true,

                        Children =
                        {
                            new Row
                            {
                                IsPlatformEnabled = true,
                                Children = {
                                    new MockView(20)
                                }
                            },
                            (row1 = new Row
                            {
                                IsPlatformEnabled = true,
                                HeightSpecifications = new ResponsiveRowHieghtSpecification(xs: new RowHeightSpecification(3, RowHeightUnit.Fill)),
                                Children =
                                {
                                    (view = new MockView(20) {
                                        IsPlatformEnabled = true,
                                    })
                                }
                            }),
                            (row2 = new Row
                            {
                                IsPlatformEnabled = true,
                                HeightSpecifications = new ResponsiveRowHieghtSpecification(xs: new RowHeightSpecification(1, RowHeightUnit.Fill)),
                                Children =
                                {
                                    (view = new MockView(20) {
                                        IsPlatformEnabled = true,
                                    })
                                }
                            }),
                            new Row
                            {
                                IsPlatformEnabled = true,
                                Children = {
                                    new MockView(20)
                                }
                            },
                        }
                    })
                }
            };

            grid.Layout(new Rectangle(Point.Zero, WindowSize));

            container.Bounds.Should().Be(new Rectangle(Point.Zero, WindowSize));
            row1.Bounds.Should().Be(new Rectangle(0, 20, 1200, (container.Height - 40) * 0.75));
            row2.Bounds.Should().Be(new Rectangle(0, ((container.Height - 40) * 0.75) + 20, 1200, (container.Height - 40) * 0.25));
            view.Bounds.Should().Be(new Rectangle(0, 0, 100, 20));
        }

        [Theory]
        [InlineData(576, 20)]
        [InlineData(768, 30)]
        [InlineData(992, 40)]
        [InlineData(1200, 50)]
        [InlineData(2400, 60)]
        public void ShouldResponvielySelectRowHieght(double windowWidth, double expectedHeight)
        {
            Container container;
            Row row;
            MockView view;

            UseWindowWidth(windowWidth);

            var grid = new Grid
            {
                IsPlatformEnabled = true,
                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }
                },

                Children =
                {
                    (container = new Container
                    {
                        IsPlatformEnabled = true,

                        Children =
                        {
                            (row = new Row
                            {
                                IsPlatformEnabled = true,
                                Children =
                                {
                                    (view = new MockView(20) {
                                        IsPlatformEnabled = true,
                                    })
                                },
                                HeightSpecifications = new ResponsiveRowHieghtSpecification(
                                    xs: new RowHeightSpecification(20, RowHeightUnit.Pixels),
                                    sm: new RowHeightSpecification(30, RowHeightUnit.Pixels),
                                    md: new RowHeightSpecification(40, RowHeightUnit.Pixels),
                                    lg: new RowHeightSpecification(50, RowHeightUnit.Pixels),
                                    xl: new RowHeightSpecification(60, RowHeightUnit.Pixels))
                            })
                        }
                    })
                }
            };

            grid.Layout(new Rectangle(Point.Zero, WindowSize));

            row.Bounds.Height.Should().Be(expectedHeight);
        }

        [Fact]
        public void ShouldDefaultToAutoWithSetToFillWithUnconstrainedHeight()
        {
            Container container;
            Row row;

            UseWindowWidth(1200);

            container = new Container
            {
                IsPlatformEnabled = true,

                Children =
                {
                    new Row
                    {
                        IsPlatformEnabled = true,
                        Children =
                        {
                            new MockView(50)
                            {
                                IsPlatformEnabled = true,
                            }
                        }
                    },

                    (row = new Row
                    {
                        IsPlatformEnabled = true,
                        Children =
                        {
                            new MockView(20)
                            {
                                IsPlatformEnabled = true,
                            }
                        },
                        HeightSpecifications = new ResponsiveRowHieghtSpecification(
                            xs: new RowHeightSpecification(1, RowHeightUnit.Fill))
                    })
                }
            };

            container.Layout(new Rectangle(Point.Zero, new Size(1200, double.PositiveInfinity)));

            row.Bounds.Height.Should().Be(20);
        }
    }
}
