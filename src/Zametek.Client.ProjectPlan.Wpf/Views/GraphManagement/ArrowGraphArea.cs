﻿using GraphX.Controls;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Zametek.Common.ProjectPlan;
using Zametek.Utility;
using System.ComponentModel;
using Zametek.Common.Project;

namespace Zametek.Client.ProjectPlan.Wpf
{
    public class ArrowGraphArea
        : GraphArea<ArrowGraphVertex, ArrowGraphEdge, BidirectionalGraph<ArrowGraphVertex, ArrowGraphEdge>>
    {
        #region Public Methods

        public DiagramArrowGraphDto ToDiagramArrowGraphDto()
        {
            // Add nodes.
            IList<VertexControl> vertexControls = VertexList.Values.ToList();
            var nodes = vertexControls.Select(BuildDiagramNodeDto).ToList();

            // Add edges.
            IList<EdgeControl> edgeControls = EdgesList.Values.ToList();
            var edges = edgeControls.Select(BuildDiagramEdgeDto).ToList();

            return new DiagramArrowGraphDto
            {
                Nodes = nodes,
                Edges = edges
            };
        }

        #endregion

        #region Private Methods

        private static DiagramNodeDto BuildDiagramNodeDto(VertexControl vertexControl)
        {
            if (vertexControl == null)
            {
                throw new ArgumentNullException(nameof(vertexControl));
            }
            var outputNodeDto = new DiagramNodeDto();
            var node = vertexControl.Vertex as ArrowGraphVertex;
            if (node != null)
            {
                outputNodeDto.Id = Convert.ToInt32(node.ID);
                Point point = vertexControl.GetPosition();
                outputNodeDto.X = point.X;
                outputNodeDto.Y = point.Y;
                outputNodeDto.Height = vertexControl.ActualHeight;
                outputNodeDto.Width = vertexControl.ActualWidth;
                Color fillColor = ((SolidColorBrush)vertexControl.Background).Color;
                outputNodeDto.FillColorHexCode =
                    DtoConverter.HexConverter(fillColor.R, fillColor.G, fillColor.B);
                Color borderColor = ((SolidColorBrush)vertexControl.BorderBrush).Color;
                outputNodeDto.BorderColorHexCode =
                    DtoConverter.HexConverter(borderColor.R, borderColor.G, borderColor.B);
                outputNodeDto.Text = node.ToString();
            }
            return outputNodeDto;
        }

        private static DiagramEdgeDto BuildDiagramEdgeDto(EdgeControl edgeControl)
        {
            if (edgeControl == null)
            {
                throw new ArgumentNullException(nameof(edgeControl));
            }
            var outputEdge = new DiagramEdgeDto();
            var edge = edgeControl.Edge as ArrowGraphEdge;
            if (edge != null)
            {
                outputEdge.Id = Convert.ToInt32(edge.ID);
                outputEdge.Name = edge.Name;
                outputEdge.SourceId = Convert.ToInt32(edge.Source.ID);
                outputEdge.TargetId = Convert.ToInt32(edge.Target.ID);
                Common.Project.EdgeDashStyle dashStyle = Common.Project.EdgeDashStyle.Normal;
                edge.DashStyle.ValueSwitchOn()
                    .Case(GraphX.Controls.EdgeDashStyle.Solid, x => dashStyle = Common.Project.EdgeDashStyle.Normal)
                    .Case(GraphX.Controls.EdgeDashStyle.Dash, x => dashStyle = Common.Project.EdgeDashStyle.Dashed)
                    .Default(x =>
                    {
                        throw new InvalidEnumArgumentException("Unknown EdgeDashStyle value");
                    });
                outputEdge.DashStyle = dashStyle;
                Color foregroundColor = ((SolidColorBrush)edgeControl.Foreground).Color;
                outputEdge.ForegroundColorHexCode =
                    DtoConverter.HexConverter(foregroundColor.R, foregroundColor.G, foregroundColor.B);
                outputEdge.StrokeThickness = edge.StrokeThickness;
                var labelText = new StringBuilder();
                if (edge.IsDummy)
                {
                    if (!edge.CanBeRemoved)
                    {
                        labelText.AppendFormat("{0}", edge.ID);
                        if (!edge.IsCritical)
                        {
                            labelText.AppendLine();
                            labelText.AppendFormat("{0}|{1}", edge.FreeSlack, edge.TotalSlack);
                        }
                        outputEdge.ShowLabel = true;
                    }
                    else
                    {
                        if (!edge.IsCritical)
                        {
                            labelText.AppendFormat("{0}|{1}", edge.FreeSlack, edge.TotalSlack);
                            outputEdge.ShowLabel = true;
                        }
                    }
                }
                else
                {
                    labelText.AppendFormat("{0} ({1})", edge.ID, edge.Duration);
                    if (!edge.IsCritical)
                    {
                        labelText.AppendLine();
                        labelText.AppendFormat("{0}|{1}", edge.FreeSlack, edge.TotalSlack);
                    }
                    outputEdge.ShowLabel = true;
                }
                outputEdge.Label = labelText.ToString();
            }
            return outputEdge;
        }

        #endregion
    }
}
