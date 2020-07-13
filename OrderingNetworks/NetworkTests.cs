using FluentAssertions;
using Xunit;

namespace UpdatingFactories
{
    public class NetworkTests
    {
        private Network Network { get; } = new Network();

        [Fact]
        public void SimpleCase1()
        {
            // Arrange
            var node1 = Network.AddRootNode();
            var node2 = Network.AddRootNode();
            var node3 = Network.AddRootNode();

            Network.Connect(node1, node2);

            // Act
            Network.Connect(node3, node2);

            // Assert
            Network.Nodes.IndexOf(node1).Should().Be(0);
            Network.Nodes.IndexOf(node2).Should().Be(2);
            Network.Nodes.IndexOf(node3).Should().Be(1);
        }

        [Fact]
        public void ForwardCorrect1()
        {
            // Arrange
            var node0 = Network.AddRootNode();
            var node1 = Network.AddRootNode();
            var node2 = Network.AddRootNode();
            var node3 = Network.AddRootNode();

            Network.Connect(node0, node1);
            Network.Connect(node1, node2);

            // Act
            Network.Connect(node3, node1);

            // Assert
            Network.Nodes.IndexOf(node0).Should().Be(0);
            Network.Nodes.IndexOf(node1).Should().Be(2);
            Network.Nodes.IndexOf(node2).Should().Be(3);
            Network.Nodes.IndexOf(node3).Should().Be(1);
        }

        [Fact]
        public void ForwardCorrect2()
        {
            // Arrange
            var node0 = Network.AddRootNode();
            var node1 = Network.AddRootNode();
            var node2 = Network.AddRootNode();
            var node3 = Network.AddRootNode();
            var node4 = Network.AddRootNode();

            Network.Connect(node0, node1);
            // deliberately connect node 3 to node 1 before connecting node 2 to node 1
            Network.Connect(node1, node3);
            Network.Connect(node1, node2);

            // Act
            Network.Connect(node4, node1);

            // Assert
            Network.Nodes.IndexOf(node0).Should().Be(0);
            Network.Nodes.IndexOf(node1).Should().Be(2);
            Network.Nodes.IndexOf(node2).Should().Be(4);
            Network.Nodes.IndexOf(node3).Should().Be(3);
            Network.Nodes.IndexOf(node4).Should().Be(1);
        }

        [Fact]
        public void ForwardCorrect3()
        {
            // Arrange
            var node0 = Network.AddRootNode();
            var node1 = Network.AddRootNode();
            var node2 = Network.AddRootNode();
            var node3 = Network.AddRootNode();
            var node4 = Network.AddRootNode();
            var node5 = Network.AddRootNode();

            Network.Connect(node0, node1);
            Network.Connect(node1, node3);
            Network.Connect(node1, node2);
            Network.Connect(node2, node4);

            // Act
            Network.Connect(node5, node1);

            // Assert
            Network.Nodes.IndexOf(node0).Should().Be(0);
            Network.Nodes.IndexOf(node1).Should().Be(2);
            Network.Nodes.IndexOf(node2).Should().Be(4);
            Network.Nodes.IndexOf(node3).Should().Be(3);
            Network.Nodes.IndexOf(node4).Should().Be(5);
            Network.Nodes.IndexOf(node5).Should().Be(1);
        }

        [Fact]
        public void BackwardsCorrect1()
        {
            // Arrange
            var node0 = Network.AddRootNode();
            var node1 = Network.AddRootNode();
            var node2 = Network.AddRootNode();
            var node3 = Network.AddRootNode();
            var node4 = Network.AddRootNode();

            Network.Connect(node0, node1);
            Network.Connect(node1, node2);

            Network.Connect(node3, node4);

            // Act
            Network.Connect(node4, node2);

            // Assert
            Network.Nodes.IndexOf(node0).Should().Be(0);
            Network.Nodes.IndexOf(node1).Should().Be(1);
            Network.Nodes.IndexOf(node2).Should().Be(4);
            Network.Nodes.IndexOf(node3).Should().Be(2);
            Network.Nodes.IndexOf(node4).Should().Be(3);
        }

        [Fact]
        public void BackwardsCorrect2()
        {
            // Arrange
            var node0 = Network.AddRootNode();
            var node1 = Network.AddRootNode();
            var node2 = Network.AddRootNode();
            var node3 = Network.AddRootNode();
            var node4 = Network.AddRootNode();
            var node5 = Network.AddRootNode();

            Network.Connect(node0, node1);
            Network.Connect(node1, node2);

            Network.Connect(node3, node5);
            Network.Connect(node4, node5);

            // Act
            Network.Connect(node5, node2);

            // Assert
            Network.Nodes.IndexOf(node0).Should().Be(0);
            Network.Nodes.IndexOf(node1).Should().Be(1);
            Network.Nodes.IndexOf(node2).Should().Be(5);
            Network.Nodes.IndexOf(node3).Should().Be(3);
            Network.Nodes.IndexOf(node4).Should().Be(2);
            Network.Nodes.IndexOf(node5).Should().Be(4);
        }

        [Fact]
        public void BackwardsCorrect3()
        {
            // Arrange
            var node0 = Network.AddRootNode();
            var node1 = Network.AddRootNode();
            var node2 = Network.AddRootNode();
            var node3 = Network.AddRootNode();
            var node4 = Network.AddRootNode();
            var node5 = Network.AddRootNode();
            var node6 = Network.AddRootNode();

            Network.Connect(node0, node1);
            Network.Connect(node1, node2);

            Network.Connect(node3, node4);
            Network.Connect(node3, node5);
            Network.Connect(node4, node6);
            Network.Connect(node5, node6);

            // Act
            Network.Connect(node6, node2);

            // Assert
            Network.Nodes.IndexOf(node0).Should().Be(0);
            Network.Nodes.IndexOf(node1).Should().Be(1);
            Network.Nodes.IndexOf(node2).Should().Be(6);
            Network.Nodes.IndexOf(node3).Should().Be(2);
            Network.Nodes.IndexOf(node4).Should().Be(4);
            Network.Nodes.IndexOf(node5).Should().Be(3);
            Network.Nodes.IndexOf(node6).Should().Be(5);
        }

        [Fact]
        public void BackwardsCorrect4()
        {
            // Arrange
            var node0 = Network.AddRootNode();
            var node1 = Network.AddRootNode();
            var node2 = Network.AddRootNode();
            var node3 = Network.AddRootNode();
            var node4 = Network.AddRootNode();
            var node5 = Network.AddRootNode();
            var node6 = Network.AddRootNode();
            var node7 = Network.AddRootNode();

            Network.Connect(node0, node1);
            Network.Connect(node1, node2);

            Network.Connect(node3, node5);
            Network.Connect(node3, node6);
            Network.Connect(node4, node6);
            Network.Connect(node5, node7);
            Network.Connect(node6, node7);

            // Act
            Network.Connect(node7, node2);

            // Assert
            Network.Nodes.IndexOf(node0).Should().Be(0);
            Network.Nodes.IndexOf(node1).Should().Be(1);
            Network.Nodes.IndexOf(node2).Should().Be(7);
            Network.Nodes.IndexOf(node3).Should().Be(3);
            Network.Nodes.IndexOf(node4).Should().Be(2);
            Network.Nodes.IndexOf(node5).Should().Be(5);
            Network.Nodes.IndexOf(node6).Should().Be(4);
            Network.Nodes.IndexOf(node7).Should().Be(6);
        }

        [Fact]
        public void SupaCorrect()
        {
            // Arrange
            var node0 = Network.AddRootNode();
            var node1 = Network.AddRootNode();
            var node2 = Network.AddRootNode();
            var node3 = Network.AddRootNode();
            var node4 = Network.AddRootNode();
            var node5 = Network.AddRootNode();
            var node6 = Network.AddRootNode();
            var node7 = Network.AddRootNode();
            var node8 = Network.AddRootNode();
            var node9 = Network.AddRootNode();

            Network.Connect(node0, node1);
            Network.Connect(node1, node2);
            Network.Connect(node1, node3);
            Network.Connect(node2, node4);
            Network.Connect(node3, node4);

            Network.Connect(node5, node7);
            Network.Connect(node5, node8);
            Network.Connect(node6, node8);
            Network.Connect(node7, node9);
            Network.Connect(node8, node9);

            // Act
            Network.Connect(node9, node1);

            // Assert
            Network.Nodes.IndexOf(node0).Should().Be(0);
            Network.Nodes.IndexOf(node1).Should().Be(6);
            Network.Nodes.IndexOf(node2).Should().Be(7);
            Network.Nodes.IndexOf(node3).Should().Be(8);
            Network.Nodes.IndexOf(node4).Should().Be(9);
            Network.Nodes.IndexOf(node5).Should().Be(2);
            Network.Nodes.IndexOf(node6).Should().Be(1);
            Network.Nodes.IndexOf(node7).Should().Be(4);
            Network.Nodes.IndexOf(node8).Should().Be(3);
            Network.Nodes.IndexOf(node9).Should().Be(5);
        }
    }
}
