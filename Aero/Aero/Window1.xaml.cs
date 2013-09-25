using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using System.Windows.Media.Animation;

namespace Aero
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        public void MoveCurrentToNext()
        {
            //向前移动，取Viewport3D的第一个Viewport2DVisual3为当前Viewport2DVisual3D
            var current = this.myViewport3D.Children[0];
            var child1 = this.myViewport3D.Children[1];
            var child2 = this.myViewport3D.Children[2];
            var child3 = this.myViewport3D.Children[3];
            var child4 = this.myViewport3D.Children[4];
            var child5 = this.myViewport3D.Children[5];

            this.myViewport3D.Children.RemoveAt(0);
            this.myViewport3D.Children.Insert(5, current);

            //Why use Children[1]? Check XAML.
            var translate = (current.Transform as Transform3DGroup).Children[1] as TranslateTransform3D;
            //对每个Viewport2DVisual3D元素应用平移动画
            //AnimationVisualElement((current as Viewport2DVisual3D).Visual as FrameworkElement, .3);
            AnimationVisualElement(translate, true, -5.0, 1.5, -20.0);

            translate = (child1.Transform as Transform3DGroup).Children[1] as TranslateTransform3D;
            AnimationVisualElement(translate, true, .0, .0, .0);

            translate = (child2.Transform as Transform3DGroup).Children[1] as TranslateTransform3D;
            AnimationVisualElement(translate, true, -1.0, 1.0, -4.0);

            translate = (child3.Transform as Transform3DGroup).Children[1] as TranslateTransform3D;
            AnimationVisualElement(translate, true, -2.0, 1.5, -8.0);

            translate = (child4.Transform as Transform3DGroup).Children[1] as TranslateTransform3D;
            AnimationVisualElement(translate, true, -3.0, 1.5, -12.0);

            translate = (child5.Transform as Transform3DGroup).Children[1] as TranslateTransform3D;
            AnimationVisualElement(translate, true, -4.0, 1.5, -16.0);
        }

        public void MoveCurrentToPrevious()
        {
            //向后移动，取Viewport3D的最后一个Viewport2DVisual3D当前Viewport2DVisual3D
            var current = this.myViewport3D.Children[5];
            var child1 = this.myViewport3D.Children[0];
            var child2 = this.myViewport3D.Children[1];
            var child3 = this.myViewport3D.Children[2];
            var child4 = this.myViewport3D.Children[3];
            var child5 = this.myViewport3D.Children[4];

            this.myViewport3D.Children.RemoveAt(5);
            this.myViewport3D.Children.Insert(0, current);

            var translate = (current.Transform as Transform3DGroup).Children[1] as TranslateTransform3D;
            //AnimationVisualElement((current as Viewport2DVisual3D).Visual as FrameworkElement,.4);
            AnimationVisualElement(translate, false, 0.0, 0.0, 0.0);

            translate = (child1.Transform as Transform3DGroup).Children[1] as TranslateTransform3D;
            AnimationVisualElement(translate, false, -1.0, 1.0, -4.0);

            translate = (child2.Transform as Transform3DGroup).Children[1] as TranslateTransform3D;
            AnimationVisualElement(translate, false, -2.0, 1.5, -8.0);

            translate = (child3.Transform as Transform3DGroup).Children[1] as TranslateTransform3D;
            AnimationVisualElement(translate, false, -3.0, 1.5, -12.0);

            translate = (child4.Transform as Transform3DGroup).Children[1] as TranslateTransform3D;
            AnimationVisualElement(translate, false, -4.0, 1.5, -16.0);

            translate = (child5.Transform as Transform3DGroup).Children[1] as TranslateTransform3D;
            AnimationVisualElement(translate, false, -5.0, 1.5, -20.0);
        }

        private void AnimationVisualElement(FrameworkElement element, double duration)
        {
            if (element == null)
                return;
            //对Visual元素的Visibility应用动画
            ObjectAnimationUsingKeyFrames objectAnimation = new ObjectAnimationUsingKeyFrames();
            objectAnimation.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Collapsed, KeyTime.FromPercent(.0)));
            objectAnimation.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Visible, KeyTime.FromPercent(1)));
            objectAnimation.Duration = TimeSpan.FromSeconds(duration);
            objectAnimation.FillBehavior = FillBehavior.Stop;
            element.BeginAnimation(FrameworkElement.VisibilityProperty, objectAnimation);
        }

        private void AnimationVisualElement(TranslateTransform3D translate, bool forward, double targetX, double targetY, double targetZ)
        {
            Duration duration = new Duration(TimeSpan.FromSeconds(.4));
            //对TranslateTransform3D的X偏移量应用动画
            DoubleAnimation animationX = new DoubleAnimation();
            animationX.To = targetX;
            animationX.Duration = duration;
            animationX.AccelerationRatio = forward ? 0 : 1;
            animationX.DecelerationRatio = forward ? 1 : 0;
            translate.BeginAnimation(TranslateTransform3D.OffsetXProperty, animationX);
            //对TranslateTransform3D的Y偏移量应用动画
            DoubleAnimation animationY = new DoubleAnimation();
            animationX.To = targetY;
            animationX.AccelerationRatio = forward ? 0.7 : 0.3;
            animationX.DecelerationRatio = forward ? 0.3 : 0.7;
            animationX.Duration = duration;
            translate.BeginAnimation(TranslateTransform3D.OffsetYProperty, animationX);
            //对TranslateTransform3D的Z偏移量应用动画
            DoubleAnimation animationZ = new DoubleAnimation();
            animationZ.To = targetZ;
            animationZ.AccelerationRatio = forward ? 0.3 : 0.7;
            animationZ.DecelerationRatio = forward ? 0.7 : 0.3;
            animationZ.Duration = duration;
            translate.BeginAnimation(TranslateTransform3D.OffsetZProperty, animationZ);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyStates == Keyboard.GetKeyStates(Key.Down) && Keyboard.Modifiers == ModifierKeys.Control)
            {
                //向前移动Visual元素
                this.MoveCurrentToNext();
            }
            else if (e.KeyStates == Keyboard.GetKeyStates(Key.Up) && Keyboard.Modifiers == ModifierKeys.Control)
            {
                //向后移动Visual元素
                this.MoveCurrentToPrevious();
            }
            else if (e.KeyStates == Keyboard.GetKeyStates(Key.Escape))
            {
                //注销
                Application.Current.Shutdown();
            }
        }
    }
}
