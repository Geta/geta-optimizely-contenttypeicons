﻿using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using EPiServer.Shell;
using FakeItEasy;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration;
using Geta.Optimizely.ContentTypeIcons.Tests.Models;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Initialization;
using Microsoft.Extensions.Options;
using Xunit;

namespace Geta.Optimizely.ContentTypeIcons.Tests
{
    public class TreeIconUiDescriptorConfigurationTests
    {
        [Fact]
        public void Enabled_PageWithoutContentTypeIcon_NotInUse()
        {
            // Arrange
            Setup<PageWithoutContentTypeIcon>(
                globallyEnabled: true,
                out var descriptorConfiguration,
                out var descriptor);

            // Act
            descriptorConfiguration.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Null(descriptor.IconClass);
            Assert.False(TreeIconUiDescriptorConfiguration.EnabledAndInUse);
        }

        [Fact]
        public void Disabled_PageWithoutContentTypeIcon_NotInUse()
        {
            // Arrange
            Setup<PageWithoutContentTypeIcon>(
                globallyEnabled: false,
                out var descriptorConfiguration,
                out var descriptor);

            // Act
            descriptorConfiguration.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Null(descriptor.IconClass);
            Assert.False(TreeIconUiDescriptorConfiguration.EnabledAndInUse);
        }

        [Fact]
        public void Enabled_PageWithOnlyContentTypeIcon_InUse()
        {
            // Arrange
            Setup<PageWithOnlyContentTypeIcon>(
                globallyEnabled: true,
                out var descriptorConfiguration,
                out var descriptor);

            // Act
            descriptorConfiguration.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Equal("fas fa-road fa-fw", descriptor.IconClass);
            Assert.True(TreeIconUiDescriptorConfiguration.EnabledAndInUse);
        }

        [Fact]
        public void Disabled_PageWithOnlyContentTypeIcon_NotInUse()
        {
            // Arrange
            Setup<PageWithOnlyContentTypeIcon>(
                globallyEnabled: false,
                out var descriptorConfiguration,
                out var descriptor);

            // Act
            descriptorConfiguration.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Null(descriptor.IconClass);
            Assert.False(TreeIconUiDescriptorConfiguration.EnabledAndInUse);
        }

        [Fact]
        public void Enabled_PageWithContentTypeIconAndTreeIcon_InUse()
        {
            // Arrange
            Setup<PageWithContentTypeIconAndTreeIcon>(
                globallyEnabled: true,
                out var descriptorConfiguration,
                out var descriptor);

            // Act
            descriptorConfiguration.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Equal("fas fa-anchor fa-fw", descriptor.IconClass);
            Assert.True(TreeIconUiDescriptorConfiguration.EnabledAndInUse);
        }

        [Fact]
        public void Disabled_PageWithContentTypeIconAndTreeIcon_NotInUse()
        {
            // Arrange
            Setup<PageWithContentTypeIconAndTreeIcon>(
                globallyEnabled: false,
                out var descriptorConfiguration,
                out var descriptor);

            // Act
            descriptorConfiguration.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Null(descriptor.IconClass);
            Assert.False(TreeIconUiDescriptorConfiguration.EnabledAndInUse);
        }

        [Fact]
        public void Enabled_PageWithContentTypeIconAndTreeIconOnIgnore_NotInUse()
        {
            // Arrange
            Setup<PageWithContentTypeIconAndTreeIconOnIgnore>(
                globallyEnabled: true,
                out var descriptorConfiguration,
                out var descriptor);

            // Act
            descriptorConfiguration.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Null(descriptor.IconClass);
            Assert.False(TreeIconUiDescriptorConfiguration.EnabledAndInUse);
        }

        [Fact]
        public void Disabled_PageWithContentTypeIconAndTreeIconOnIgnore_NotInUse()
        {
            // Arrange
            Setup<PageWithContentTypeIconAndTreeIconOnIgnore>(
                globallyEnabled: false,
                out var descriptorConfiguration,
                out var descriptor);

            // Act
            descriptorConfiguration.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Null(descriptor.IconClass);
            Assert.False(TreeIconUiDescriptorConfiguration.EnabledAndInUse);
        }

        [Fact]
        public void Enabled_PageWithOnlyTreeIconWithoutIcon_NotInUse()
        {
            // Arrange
            Setup<PageWithOnlyTreeIconWithoutIcon>(
                globallyEnabled: true,
                out var descriptorConfiguration,
                out var descriptor);

            // Act
            descriptorConfiguration.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Null(descriptor.IconClass);
            Assert.False(TreeIconUiDescriptorConfiguration.EnabledAndInUse);
        }

        [Fact]
        public void Enabled_PageWithOnlyTreeIcon_InUse()
        {
            // Arrange
            Setup<PageWithOnlyTreeIcon>(
                globallyEnabled: true,
                out var descriptorConfiguration,
                out var descriptor);

            // Act
            descriptorConfiguration.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Equal("fas fa-road fa-fw", descriptor.IconClass);
            Assert.True(TreeIconUiDescriptorConfiguration.EnabledAndInUse);
        }

        [Fact]
        public void Enabled_PageWithContentTypeIconAndDifferentTreeIcon_InUse()
        {
            // Arrange
            Setup<PageWithContentTypeIconAndDifferentTreeIcon>(
                globallyEnabled: true,
                out var descriptorConfiguration,
                out var descriptor);

            // Act
            descriptorConfiguration.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Equal("far fa-clock fa-fw", descriptor.IconClass);
            Assert.True(TreeIconUiDescriptorConfiguration.EnabledAndInUse);
        }

        [Fact]
        public void Enabled_MediaDataWithOnlyContentTypeIcon_InUse()
        {
            // Arrange
            Setup<MediaDataWithOnlyContentTypeIcon>(
                globallyEnabled: true,
                out var descriptorConfiguration,
                out var descriptor);

            // Act
            descriptorConfiguration.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Equal("fas fa-images fa-fw", descriptor.IconClass);
            Assert.True(TreeIconUiDescriptorConfiguration.EnabledAndInUse);
        }

        [Fact]
        public void Enabled_ImageDataWithOnlyContentTypeIcon_InUse()
        {
            // Arrange
            Setup<ImageDataWithOnlyContentTypeIcon>(
                globallyEnabled: true,
                out var descriptorConfiguration,
                out var descriptor);

            // Act
            descriptorConfiguration.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Equal("fas fa-image fa-fw", descriptor.IconClass);
            Assert.True(TreeIconUiDescriptorConfiguration.EnabledAndInUse);
        }

        [Fact]
        public void Enabled_PageWithContentTypeIconAndInheritedTreeIcon_InUse()
        {
            // Arrange
            Setup<PageWithContentTypeIconAndInheritedTreeIcon>(
                globallyEnabled: true,
                out var descriptorConfiguration,
                out var descriptor);

            // Act
            descriptorConfiguration.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Equal("fas fa-box-open fa-fw", descriptor.IconClass);
            Assert.True(TreeIconUiDescriptorConfiguration.EnabledAndInUse);
        }

        [Fact]
        public void Enabled_PageWithContentTypeIconAndRotation_Should_Rotate()
        {
            // Arrange
            Setup<PageWithContentTypeIconAndRotation>(
                globallyEnabled: true,
                out var descriptorConfiguration,
                out var descriptor);

            // Act
            descriptorConfiguration.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.True(TreeIconUiDescriptorConfiguration.EnabledAndInUse);
            Assert.Contains("fa-rotate-180", descriptor.IconClass);
        }

        [Fact]
        public void Enabled_PageWithTreeIconAmdRotation_Should_Rotate()
        {
            // Arrange
            Setup<PageWithTreeIconAmdRotation>(
                globallyEnabled: true,
                out var descriptorConfiguration,
                out var descriptor);

            // Act
            descriptorConfiguration.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.True(TreeIconUiDescriptorConfiguration.EnabledAndInUse);
            Assert.Contains("fa-rotate-90", descriptor.IconClass);
        }

        [Fact]
        public void Enabled_PageWithContentTypeIconAndDifferentTreeIconRotation_TreeIcon_Should_TakePrecedence()
        {
            // Arrange
            Setup<PageWithContentTypeIconAndDifferentTreeIconRotation>(
                globallyEnabled: true,
                out var descriptorConfiguration,
                out var descriptor);

            // Act
            descriptorConfiguration.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.True(TreeIconUiDescriptorConfiguration.EnabledAndInUse);
            Assert.Contains("fa-flag", descriptor.IconClass);
            Assert.Contains("fa-rotate-90", descriptor.IconClass);
        }

        private static void Setup<TType>(
            bool globallyEnabled,
            out TreeIconUiDescriptorConfiguration descriptorConfiguration,
            out UIDescriptor descriptor)
            where TType : IContent
        {
            var serviceProvider = A.Fake<IServiceProvider>();
            var contentTypeRepository = A.Fake<IContentTypeRepository>();
            var localizationService = A.Fake<LocalizationService>();
            A.CallTo(() => serviceProvider.GetService(typeof(IContentTypeRepository)))
                .Returns(contentTypeRepository);
            A.CallTo(() => serviceProvider.GetService(typeof(IEnumerable<ViewConfiguration>)))
                .Returns(Enumerable.Empty<ViewConfiguration>());
            A.CallTo(() => serviceProvider.GetService(typeof(LocalizationService))).Returns(localizationService);

            ServiceLocator.SetServiceProvider(serviceProvider);

            descriptor = new UIDescriptor(typeof(TType));
            var descriptorRegistry = new UIDescriptorRegistry(new[] { descriptor }, null, null);
            var configuration = new ContentTypeIconOptions { EnableTreeIcons = globallyEnabled };
            var options = new OptionsWrapper<ContentTypeIconOptions>(configuration);
            descriptorConfiguration = new TreeIconUiDescriptorConfiguration(descriptorRegistry, options);
            descriptorConfiguration.Initialize();
        }
    }
}
