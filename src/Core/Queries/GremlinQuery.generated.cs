﻿#nullable enable
using System.Linq.Expressions;
using Gremlin.Net.Process.Traversal;

namespace ExRam.Gremlinq.Core
{
    partial class GremlinQuery<TElement, TOutVertex, TInVertex, TScalar, TMeta, TFoldedQuery>
    {
        IMapGremlinQuery<(T1, T2)> IGremlinQueryBase.Select<T1, T2>(StepLabel<T1> label1, StepLabel<T2> label2) => Project<(T1, T2)>(p => p.ToTuple().By(__ => __.Select(label1)).By(__ => __.Select(label2)));

        IMapGremlinQuery<(T1, T2)> IMapGremlinQueryBase<TElement>.Select<T1, T2>(Expression<Func<TElement, T1>> projection1, Expression<Func<TElement, T2>> projection2) => Select<IMapGremlinQuery<(T1, T2)>>(projection1, projection2);
        IMapGremlinQuery<(T1, T2, T3)> IGremlinQueryBase.Select<T1, T2, T3>(StepLabel<T1> label1, StepLabel<T2> label2, StepLabel<T3> label3) => Project<(T1, T2, T3)>(p => p.ToTuple().By(__ => __.Select(label1)).By(__ => __.Select(label2)).By(__ => __.Select(label3)));

        IMapGremlinQuery<(T1, T2, T3)> IMapGremlinQueryBase<TElement>.Select<T1, T2, T3>(Expression<Func<TElement, T1>> projection1, Expression<Func<TElement, T2>> projection2, Expression<Func<TElement, T3>> projection3) => Select<IMapGremlinQuery<(T1, T2, T3)>>(projection1, projection2, projection3);
        IMapGremlinQuery<(T1, T2, T3, T4)> IGremlinQueryBase.Select<T1, T2, T3, T4>(StepLabel<T1> label1, StepLabel<T2> label2, StepLabel<T3> label3, StepLabel<T4> label4) => Project<(T1, T2, T3, T4)>(p => p.ToTuple().By(__ => __.Select(label1)).By(__ => __.Select(label2)).By(__ => __.Select(label3)).By(__ => __.Select(label4)));

        IMapGremlinQuery<(T1, T2, T3, T4)> IMapGremlinQueryBase<TElement>.Select<T1, T2, T3, T4>(Expression<Func<TElement, T1>> projection1, Expression<Func<TElement, T2>> projection2, Expression<Func<TElement, T3>> projection3, Expression<Func<TElement, T4>> projection4) => Select<IMapGremlinQuery<(T1, T2, T3, T4)>>(projection1, projection2, projection3, projection4);
        IMapGremlinQuery<(T1, T2, T3, T4, T5)> IGremlinQueryBase.Select<T1, T2, T3, T4, T5>(StepLabel<T1> label1, StepLabel<T2> label2, StepLabel<T3> label3, StepLabel<T4> label4, StepLabel<T5> label5) => Project<(T1, T2, T3, T4, T5)>(p => p.ToTuple().By(__ => __.Select(label1)).By(__ => __.Select(label2)).By(__ => __.Select(label3)).By(__ => __.Select(label4)).By(__ => __.Select(label5)));

        IMapGremlinQuery<(T1, T2, T3, T4, T5)> IMapGremlinQueryBase<TElement>.Select<T1, T2, T3, T4, T5>(Expression<Func<TElement, T1>> projection1, Expression<Func<TElement, T2>> projection2, Expression<Func<TElement, T3>> projection3, Expression<Func<TElement, T4>> projection4, Expression<Func<TElement, T5>> projection5) => Select<IMapGremlinQuery<(T1, T2, T3, T4, T5)>>(projection1, projection2, projection3, projection4, projection5);
        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6)> IGremlinQueryBase.Select<T1, T2, T3, T4, T5, T6>(StepLabel<T1> label1, StepLabel<T2> label2, StepLabel<T3> label3, StepLabel<T4> label4, StepLabel<T5> label5, StepLabel<T6> label6) => Project<(T1, T2, T3, T4, T5, T6)>(p => p.ToTuple().By(__ => __.Select(label1)).By(__ => __.Select(label2)).By(__ => __.Select(label3)).By(__ => __.Select(label4)).By(__ => __.Select(label5)).By(__ => __.Select(label6)));

        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6)> IMapGremlinQueryBase<TElement>.Select<T1, T2, T3, T4, T5, T6>(Expression<Func<TElement, T1>> projection1, Expression<Func<TElement, T2>> projection2, Expression<Func<TElement, T3>> projection3, Expression<Func<TElement, T4>> projection4, Expression<Func<TElement, T5>> projection5, Expression<Func<TElement, T6>> projection6) => Select<IMapGremlinQuery<(T1, T2, T3, T4, T5, T6)>>(projection1, projection2, projection3, projection4, projection5, projection6);
        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7)> IGremlinQueryBase.Select<T1, T2, T3, T4, T5, T6, T7>(StepLabel<T1> label1, StepLabel<T2> label2, StepLabel<T3> label3, StepLabel<T4> label4, StepLabel<T5> label5, StepLabel<T6> label6, StepLabel<T7> label7) => Project<(T1, T2, T3, T4, T5, T6, T7)>(p => p.ToTuple().By(__ => __.Select(label1)).By(__ => __.Select(label2)).By(__ => __.Select(label3)).By(__ => __.Select(label4)).By(__ => __.Select(label5)).By(__ => __.Select(label6)).By(__ => __.Select(label7)));

        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7)> IMapGremlinQueryBase<TElement>.Select<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<TElement, T1>> projection1, Expression<Func<TElement, T2>> projection2, Expression<Func<TElement, T3>> projection3, Expression<Func<TElement, T4>> projection4, Expression<Func<TElement, T5>> projection5, Expression<Func<TElement, T6>> projection6, Expression<Func<TElement, T7>> projection7) => Select<IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7)>>(projection1, projection2, projection3, projection4, projection5, projection6, projection7);
        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8)> IGremlinQueryBase.Select<T1, T2, T3, T4, T5, T6, T7, T8>(StepLabel<T1> label1, StepLabel<T2> label2, StepLabel<T3> label3, StepLabel<T4> label4, StepLabel<T5> label5, StepLabel<T6> label6, StepLabel<T7> label7, StepLabel<T8> label8) => Project<(T1, T2, T3, T4, T5, T6, T7, T8)>(p => p.ToTuple().By(__ => __.Select(label1)).By(__ => __.Select(label2)).By(__ => __.Select(label3)).By(__ => __.Select(label4)).By(__ => __.Select(label5)).By(__ => __.Select(label6)).By(__ => __.Select(label7)).By(__ => __.Select(label8)));

        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8)> IMapGremlinQueryBase<TElement>.Select<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<TElement, T1>> projection1, Expression<Func<TElement, T2>> projection2, Expression<Func<TElement, T3>> projection3, Expression<Func<TElement, T4>> projection4, Expression<Func<TElement, T5>> projection5, Expression<Func<TElement, T6>> projection6, Expression<Func<TElement, T7>> projection7, Expression<Func<TElement, T8>> projection8) => Select<IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8)>>(projection1, projection2, projection3, projection4, projection5, projection6, projection7, projection8);
        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> IGremlinQueryBase.Select<T1, T2, T3, T4, T5, T6, T7, T8, T9>(StepLabel<T1> label1, StepLabel<T2> label2, StepLabel<T3> label3, StepLabel<T4> label4, StepLabel<T5> label5, StepLabel<T6> label6, StepLabel<T7> label7, StepLabel<T8> label8, StepLabel<T9> label9) => Project<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>(p => p.ToTuple().By(__ => __.Select(label1)).By(__ => __.Select(label2)).By(__ => __.Select(label3)).By(__ => __.Select(label4)).By(__ => __.Select(label5)).By(__ => __.Select(label6)).By(__ => __.Select(label7)).By(__ => __.Select(label8)).By(__ => __.Select(label9)));

        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> IMapGremlinQueryBase<TElement>.Select<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Expression<Func<TElement, T1>> projection1, Expression<Func<TElement, T2>> projection2, Expression<Func<TElement, T3>> projection3, Expression<Func<TElement, T4>> projection4, Expression<Func<TElement, T5>> projection5, Expression<Func<TElement, T6>> projection6, Expression<Func<TElement, T7>> projection7, Expression<Func<TElement, T8>> projection8, Expression<Func<TElement, T9>> projection9) => Select<IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>>(projection1, projection2, projection3, projection4, projection5, projection6, projection7, projection8, projection9);
        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> IGremlinQueryBase.Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(StepLabel<T1> label1, StepLabel<T2> label2, StepLabel<T3> label3, StepLabel<T4> label4, StepLabel<T5> label5, StepLabel<T6> label6, StepLabel<T7> label7, StepLabel<T8> label8, StepLabel<T9> label9, StepLabel<T10> label10) => Project<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)>(p => p.ToTuple().By(__ => __.Select(label1)).By(__ => __.Select(label2)).By(__ => __.Select(label3)).By(__ => __.Select(label4)).By(__ => __.Select(label5)).By(__ => __.Select(label6)).By(__ => __.Select(label7)).By(__ => __.Select(label8)).By(__ => __.Select(label9)).By(__ => __.Select(label10)));

        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> IMapGremlinQueryBase<TElement>.Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Expression<Func<TElement, T1>> projection1, Expression<Func<TElement, T2>> projection2, Expression<Func<TElement, T3>> projection3, Expression<Func<TElement, T4>> projection4, Expression<Func<TElement, T5>> projection5, Expression<Func<TElement, T6>> projection6, Expression<Func<TElement, T7>> projection7, Expression<Func<TElement, T8>> projection8, Expression<Func<TElement, T9>> projection9, Expression<Func<TElement, T10>> projection10) => Select<IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)>>(projection1, projection2, projection3, projection4, projection5, projection6, projection7, projection8, projection9, projection10);
        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)> IGremlinQueryBase.Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(StepLabel<T1> label1, StepLabel<T2> label2, StepLabel<T3> label3, StepLabel<T4> label4, StepLabel<T5> label5, StepLabel<T6> label6, StepLabel<T7> label7, StepLabel<T8> label8, StepLabel<T9> label9, StepLabel<T10> label10, StepLabel<T11> label11) => Project<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)>(p => p.ToTuple().By(__ => __.Select(label1)).By(__ => __.Select(label2)).By(__ => __.Select(label3)).By(__ => __.Select(label4)).By(__ => __.Select(label5)).By(__ => __.Select(label6)).By(__ => __.Select(label7)).By(__ => __.Select(label8)).By(__ => __.Select(label9)).By(__ => __.Select(label10)).By(__ => __.Select(label11)));

        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)> IMapGremlinQueryBase<TElement>.Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Expression<Func<TElement, T1>> projection1, Expression<Func<TElement, T2>> projection2, Expression<Func<TElement, T3>> projection3, Expression<Func<TElement, T4>> projection4, Expression<Func<TElement, T5>> projection5, Expression<Func<TElement, T6>> projection6, Expression<Func<TElement, T7>> projection7, Expression<Func<TElement, T8>> projection8, Expression<Func<TElement, T9>> projection9, Expression<Func<TElement, T10>> projection10, Expression<Func<TElement, T11>> projection11) => Select<IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)>>(projection1, projection2, projection3, projection4, projection5, projection6, projection7, projection8, projection9, projection10, projection11);
        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)> IGremlinQueryBase.Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(StepLabel<T1> label1, StepLabel<T2> label2, StepLabel<T3> label3, StepLabel<T4> label4, StepLabel<T5> label5, StepLabel<T6> label6, StepLabel<T7> label7, StepLabel<T8> label8, StepLabel<T9> label9, StepLabel<T10> label10, StepLabel<T11> label11, StepLabel<T12> label12) => Project<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)>(p => p.ToTuple().By(__ => __.Select(label1)).By(__ => __.Select(label2)).By(__ => __.Select(label3)).By(__ => __.Select(label4)).By(__ => __.Select(label5)).By(__ => __.Select(label6)).By(__ => __.Select(label7)).By(__ => __.Select(label8)).By(__ => __.Select(label9)).By(__ => __.Select(label10)).By(__ => __.Select(label11)).By(__ => __.Select(label12)));

        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)> IMapGremlinQueryBase<TElement>.Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Expression<Func<TElement, T1>> projection1, Expression<Func<TElement, T2>> projection2, Expression<Func<TElement, T3>> projection3, Expression<Func<TElement, T4>> projection4, Expression<Func<TElement, T5>> projection5, Expression<Func<TElement, T6>> projection6, Expression<Func<TElement, T7>> projection7, Expression<Func<TElement, T8>> projection8, Expression<Func<TElement, T9>> projection9, Expression<Func<TElement, T10>> projection10, Expression<Func<TElement, T11>> projection11, Expression<Func<TElement, T12>> projection12) => Select<IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)>>(projection1, projection2, projection3, projection4, projection5, projection6, projection7, projection8, projection9, projection10, projection11, projection12);
        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)> IGremlinQueryBase.Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(StepLabel<T1> label1, StepLabel<T2> label2, StepLabel<T3> label3, StepLabel<T4> label4, StepLabel<T5> label5, StepLabel<T6> label6, StepLabel<T7> label7, StepLabel<T8> label8, StepLabel<T9> label9, StepLabel<T10> label10, StepLabel<T11> label11, StepLabel<T12> label12, StepLabel<T13> label13) => Project<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)>(p => p.ToTuple().By(__ => __.Select(label1)).By(__ => __.Select(label2)).By(__ => __.Select(label3)).By(__ => __.Select(label4)).By(__ => __.Select(label5)).By(__ => __.Select(label6)).By(__ => __.Select(label7)).By(__ => __.Select(label8)).By(__ => __.Select(label9)).By(__ => __.Select(label10)).By(__ => __.Select(label11)).By(__ => __.Select(label12)).By(__ => __.Select(label13)));

        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)> IMapGremlinQueryBase<TElement>.Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Expression<Func<TElement, T1>> projection1, Expression<Func<TElement, T2>> projection2, Expression<Func<TElement, T3>> projection3, Expression<Func<TElement, T4>> projection4, Expression<Func<TElement, T5>> projection5, Expression<Func<TElement, T6>> projection6, Expression<Func<TElement, T7>> projection7, Expression<Func<TElement, T8>> projection8, Expression<Func<TElement, T9>> projection9, Expression<Func<TElement, T10>> projection10, Expression<Func<TElement, T11>> projection11, Expression<Func<TElement, T12>> projection12, Expression<Func<TElement, T13>> projection13) => Select<IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)>>(projection1, projection2, projection3, projection4, projection5, projection6, projection7, projection8, projection9, projection10, projection11, projection12, projection13);
        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)> IGremlinQueryBase.Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(StepLabel<T1> label1, StepLabel<T2> label2, StepLabel<T3> label3, StepLabel<T4> label4, StepLabel<T5> label5, StepLabel<T6> label6, StepLabel<T7> label7, StepLabel<T8> label8, StepLabel<T9> label9, StepLabel<T10> label10, StepLabel<T11> label11, StepLabel<T12> label12, StepLabel<T13> label13, StepLabel<T14> label14) => Project<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)>(p => p.ToTuple().By(__ => __.Select(label1)).By(__ => __.Select(label2)).By(__ => __.Select(label3)).By(__ => __.Select(label4)).By(__ => __.Select(label5)).By(__ => __.Select(label6)).By(__ => __.Select(label7)).By(__ => __.Select(label8)).By(__ => __.Select(label9)).By(__ => __.Select(label10)).By(__ => __.Select(label11)).By(__ => __.Select(label12)).By(__ => __.Select(label13)).By(__ => __.Select(label14)));

        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)> IMapGremlinQueryBase<TElement>.Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Expression<Func<TElement, T1>> projection1, Expression<Func<TElement, T2>> projection2, Expression<Func<TElement, T3>> projection3, Expression<Func<TElement, T4>> projection4, Expression<Func<TElement, T5>> projection5, Expression<Func<TElement, T6>> projection6, Expression<Func<TElement, T7>> projection7, Expression<Func<TElement, T8>> projection8, Expression<Func<TElement, T9>> projection9, Expression<Func<TElement, T10>> projection10, Expression<Func<TElement, T11>> projection11, Expression<Func<TElement, T12>> projection12, Expression<Func<TElement, T13>> projection13, Expression<Func<TElement, T14>> projection14) => Select<IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)>>(projection1, projection2, projection3, projection4, projection5, projection6, projection7, projection8, projection9, projection10, projection11, projection12, projection13, projection14);
        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)> IGremlinQueryBase.Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(StepLabel<T1> label1, StepLabel<T2> label2, StepLabel<T3> label3, StepLabel<T4> label4, StepLabel<T5> label5, StepLabel<T6> label6, StepLabel<T7> label7, StepLabel<T8> label8, StepLabel<T9> label9, StepLabel<T10> label10, StepLabel<T11> label11, StepLabel<T12> label12, StepLabel<T13> label13, StepLabel<T14> label14, StepLabel<T15> label15) => Project<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)>(p => p.ToTuple().By(__ => __.Select(label1)).By(__ => __.Select(label2)).By(__ => __.Select(label3)).By(__ => __.Select(label4)).By(__ => __.Select(label5)).By(__ => __.Select(label6)).By(__ => __.Select(label7)).By(__ => __.Select(label8)).By(__ => __.Select(label9)).By(__ => __.Select(label10)).By(__ => __.Select(label11)).By(__ => __.Select(label12)).By(__ => __.Select(label13)).By(__ => __.Select(label14)).By(__ => __.Select(label15)));

        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)> IMapGremlinQueryBase<TElement>.Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Expression<Func<TElement, T1>> projection1, Expression<Func<TElement, T2>> projection2, Expression<Func<TElement, T3>> projection3, Expression<Func<TElement, T4>> projection4, Expression<Func<TElement, T5>> projection5, Expression<Func<TElement, T6>> projection6, Expression<Func<TElement, T7>> projection7, Expression<Func<TElement, T8>> projection8, Expression<Func<TElement, T9>> projection9, Expression<Func<TElement, T10>> projection10, Expression<Func<TElement, T11>> projection11, Expression<Func<TElement, T12>> projection12, Expression<Func<TElement, T13>> projection13, Expression<Func<TElement, T14>> projection14, Expression<Func<TElement, T15>> projection15) => Select<IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)>>(projection1, projection2, projection3, projection4, projection5, projection6, projection7, projection8, projection9, projection10, projection11, projection12, projection13, projection14, projection15);
        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)> IGremlinQueryBase.Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(StepLabel<T1> label1, StepLabel<T2> label2, StepLabel<T3> label3, StepLabel<T4> label4, StepLabel<T5> label5, StepLabel<T6> label6, StepLabel<T7> label7, StepLabel<T8> label8, StepLabel<T9> label9, StepLabel<T10> label10, StepLabel<T11> label11, StepLabel<T12> label12, StepLabel<T13> label13, StepLabel<T14> label14, StepLabel<T15> label15, StepLabel<T16> label16) => Project<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)>(p => p.ToTuple().By(__ => __.Select(label1)).By(__ => __.Select(label2)).By(__ => __.Select(label3)).By(__ => __.Select(label4)).By(__ => __.Select(label5)).By(__ => __.Select(label6)).By(__ => __.Select(label7)).By(__ => __.Select(label8)).By(__ => __.Select(label9)).By(__ => __.Select(label10)).By(__ => __.Select(label11)).By(__ => __.Select(label12)).By(__ => __.Select(label13)).By(__ => __.Select(label14)).By(__ => __.Select(label15)).By(__ => __.Select(label16)));

        IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)> IMapGremlinQueryBase<TElement>.Select<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Expression<Func<TElement, T1>> projection1, Expression<Func<TElement, T2>> projection2, Expression<Func<TElement, T3>> projection3, Expression<Func<TElement, T4>> projection4, Expression<Func<TElement, T5>> projection5, Expression<Func<TElement, T6>> projection6, Expression<Func<TElement, T7>> projection7, Expression<Func<TElement, T8>> projection8, Expression<Func<TElement, T9>> projection9, Expression<Func<TElement, T10>> projection10, Expression<Func<TElement, T11>> projection11, Expression<Func<TElement, T12>> projection12, Expression<Func<TElement, T13>> projection13, Expression<Func<TElement, T14>> projection14, Expression<Func<TElement, T15>> projection15, Expression<Func<TElement, T16>> projection16) => Select<IMapGremlinQuery<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)>>(projection1, projection2, projection3, projection4, projection5, projection6, projection7, projection8, projection9, projection10, projection11, projection12, projection13, projection14, projection15, projection16);

        IGremlinQuery<TResult> IGremlinQueryBase.Cast<TResult>() => Cast<TResult>();
        IValueGremlinQuery<TResult> IValueGremlinQueryBase.Cast<TResult>() => Cast<TResult>();
        IElementGremlinQuery<TResult> IElementGremlinQueryBase.Cast<TResult>() => Cast<TResult>();
        IEdgeOrVertexGremlinQuery<TResult> IEdgeOrVertexGremlinQueryBase.Cast<TResult>() => Cast<TResult>();
        IVertexGremlinQuery<TResult> IVertexGremlinQueryBase.Cast<TResult>() => Cast<TResult>();
        IEdgeGremlinQuery<TResult> IEdgeGremlinQueryBase.Cast<TResult>() => Cast<TResult>();
        IPropertyGremlinQuery<TResult> IPropertyGremlinQueryBase.Cast<TResult>() => Cast<TResult>();

        TTargetQuery IGremlinQueryBaseRec<TElement, IGremlinQuery<TElement>>.Aggregate<TTargetQuery>(Func<IGremlinQuery<TElement>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IGremlinQuery<TElement>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Global, continuation);
        TTargetQuery IGremlinQueryBaseRec<TElement, IGremlinQuery<TElement>>.AggregateLocal<TTargetQuery>(Func<IGremlinQuery<TElement>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IGremlinQuery<TElement>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Local, continuation);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IGremlinQuery<TElement>>.Aggregate(StepLabel<IArrayGremlinQuery<TElement[], TElement, IGremlinQuery<TElement>>, TElement[]> stepLabel) => Aggregate(Scope.Global, stepLabel);
        IGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IGremlinQuery<TElement>>.AggregateLocal(StepLabel<IArrayGremlinQuery<TElement[], TElement, IGremlinQuery<TElement>>, TElement[]> stepLabel) => Aggregate(Scope.Local, stepLabel);

        TTargetQuery IGremlinQueryBaseRec<TElement, IGremlinQuery<TElement>>.As<TTargetQuery>(Func<IGremlinQuery<TElement>, StepLabel<IGremlinQuery<TElement>, TElement>, TTargetQuery> continuation) => As<StepLabel<IGremlinQuery<TElement>, TElement>, TTargetQuery>(continuation);
        IGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IGremlinQuery<TElement>>.As(StepLabel<TElement> stepLabel) => As(stepLabel);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.And(params Func<IGremlinQuery<TElement>, IGremlinQueryBase>[] andTraversals) => And(andTraversals);

        TTargetQuery IGremlinQueryBaseRec<IGremlinQuery<TElement>>.As<TTargetQuery>(Func<IGremlinQuery<TElement>, StepLabel<IGremlinQuery<TElement>, object>, TTargetQuery> continuation) => As<StepLabel<IGremlinQuery<TElement>, object>, TTargetQuery>(continuation);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Barrier() => Barrier();

        TTargetQuery IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Choose<TTargetQuery>(Func<IGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IGremlinQuery<TElement>, TTargetQuery> trueChoice, Func<IGremlinQuery<TElement>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(traversalPredicate, trueChoice, falseChoice);
        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Choose(Func<IGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IGremlinQuery<TElement>, IGremlinQuery<TElement>> trueChoice) => Choose<IGremlinQuery<TElement>, IGremlinQuery<TElement>, IGremlinQuery<TElement>>(traversalPredicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Choose(Func<IGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IGremlinQuery<TElement>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(traversalPredicate, trueChoice);
        
        TTargetQuery IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Choose<TTargetQuery>(Func<IChooseBuilder<IGremlinQuery<TElement>>, IChooseBuilderWithCaseOrDefault<TTargetQuery>> continuation) => Choose<TTargetQuery>(continuation);

        TTargetQuery IGremlinQueryBaseRec<TElement, IGremlinQuery<TElement>>.Choose<TTargetQuery>(Expression<Func<TElement, bool>> predicate, Func<IGremlinQuery<TElement>, TTargetQuery> trueChoice, Func<IGremlinQuery<TElement>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(predicate, trueChoice, falseChoice);
        IGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IGremlinQuery<TElement>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IGremlinQuery<TElement>, IGremlinQuery<TElement>> trueChoice) => Choose<IGremlinQuery<TElement>, IGremlinQuery<TElement>, IGremlinQuery<TElement>>(predicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<TElement, IGremlinQuery<TElement>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IGremlinQuery<TElement>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(predicate, trueChoice);

        TTargetQuery IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Coalesce<TTargetQuery>(params Func<IGremlinQuery<TElement>, TTargetQuery>[] traversals) => Coalesce<TTargetQuery, TTargetQuery>(traversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Coalesce(params Func<IGremlinQuery<TElement>, IGremlinQueryBase>[] traversals) => Coalesce<IGremlinQueryBase, IValueGremlinQuery<object>>(traversals);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Coin(double probability) => Coin(probability);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.CyclicPath() => CyclicPath();

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Dedup() => DedupGlobal();

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.DedupLocal() => DedupLocal();

        TTargetQuery IGremlinQueryBaseRec<IGremlinQuery<TElement>>.FlatMap<TTargetQuery>(Func<IGremlinQuery<TElement>, TTargetQuery> mapping) => FlatMap(mapping);

        IArrayGremlinQuery<TElement[], TElement, IGremlinQuery<TElement>> IGremlinQueryBaseRec<TElement, IGremlinQuery<TElement>>.ForceArray() => CloneAs<IArrayGremlinQuery<TElement[], TElement, IGremlinQuery<TElement>>>();

        IArrayGremlinQuery<TElement[], TElement, IGremlinQuery<TElement>> IGremlinQueryBaseRec<TElement, IGremlinQuery<TElement>>.Fold() => Fold<IGremlinQuery<TElement>>();

        IValueGremlinQuery<IDictionary<TNewKey, TNewValue>> IGremlinQueryBaseRec<TElement, IGremlinQuery<TElement>>.Group<TNewKey, TNewValue>(Func<IGroupBuilder<TElement, IGremlinQuery<TElement>>, IGroupBuilderWithKeyAndValue<TNewKey, TNewValue>> groupBuilder) => Group(groupBuilder);
        IValueGremlinQuery<IDictionary<TNewKey, TElement[]>> IGremlinQueryBaseRec<TElement, IGremlinQuery<TElement>>.Group<TNewKey>(Func<IGroupBuilder<TElement, IGremlinQuery<TElement>>, IGroupBuilderWithKey<TElement, IGremlinQuery<TElement>, TNewKey>> groupBuilder) => Group(groupBuilder);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Identity() => Identity();

        IGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IGremlinQuery<TElement>>.Inject(params TElement[] elements) => Inject(elements);
        
        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Limit(long count) => LimitGlobal(count);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.LimitLocal(long count) => LimitLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Local<TTargetQuery>(Func<IGremlinQuery<TElement> , TTargetQuery> localTraversal) => Local(localTraversal);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Loop(Func<IStartLoopBuilder<IGremlinQuery<TElement>>, IFinalLoopBuilder<IGremlinQuery<TElement>>> loopBuilderTransformation) => Loop(loopBuilderTransformation);

        TTargetQuery IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Map<TTargetQuery>(Func<IGremlinQuery<TElement>, TTargetQuery> mapping) => Map(mapping);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Mute() => Mute();

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Not(Func<IGremlinQuery<TElement>, IGremlinQueryBase> notTraversal) => Not(static (__, notTraversal) => notTraversal(__), notTraversal);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.None() => None();

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Optional(Func<IGremlinQuery<TElement>, IGremlinQuery<TElement>> optionalTraversal) => Optional(optionalTraversal);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Or(params Func<IGremlinQuery<TElement>, IGremlinQueryBase>[] orTraversals) => Or(orTraversals);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IGremlinQuery<TElement>>.Order(Func<IOrderBuilder<TElement, IGremlinQuery<TElement>>, IOrderBuilderWithBy<TElement, IGremlinQuery<TElement>>> projection) => OrderGlobal(projection);
        IGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IGremlinQuery<TElement>>.OrderLocal(Func<IOrderBuilder<TElement, IGremlinQuery<TElement>>, IOrderBuilderWithBy<TElement, IGremlinQuery<TElement>>> projection) => OrderLocal(projection);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Order(Func<IOrderBuilder<IGremlinQuery<TElement>>, IOrderBuilderWithBy<IGremlinQuery<TElement>>> projection) => OrderGlobal(projection);
        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.OrderLocal(Func<IOrderBuilder<IGremlinQuery<TElement>>, IOrderBuilderWithBy<IGremlinQuery<TElement>>> projection) => OrderLocal(projection);

        IValueGremlinQuery<dynamic> IGremlinQueryBaseRec<TElement, IGremlinQuery<TElement>>.Project(Func<IProjectBuilder<IGremlinQuery<TElement>, TElement>, IProjectDynamicResult> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IGremlinQuery<TElement>>.Project<TResult>(Func<IProjectBuilder<IGremlinQuery<TElement>, TElement>, IProjectMapResult<TResult>> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IGremlinQuery<TElement>>.Project<TResult>(Func<IProjectBuilder<IGremlinQuery<TElement>, TElement>, IProjectTupleResult<TResult>> continuation) => Project<TResult>(continuation);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Range(long low, long high) => RangeGlobal(low, high);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.RangeLocal(long low, long high) => RangeLocal(low, high);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.SideEffect(Func<IGremlinQuery<TElement>, IGremlinQueryBase> sideEffectTraversal) => SideEffect(sideEffectTraversal);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.SimplePath() => SimplePath();

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Skip(long count) => Skip(count, Scope.Global);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.SkipLocal(long count) => Skip(count, Scope.Local);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Tail(long count) => TailGlobal(count);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.TailLocal(long count) => TailLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Union<TTargetQuery>(params Func<IGremlinQuery<TElement>, TTargetQuery>[] unionTraversals) => Union<TTargetQuery, TTargetQuery>(unionTraversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Union(params Func<IGremlinQuery<TElement>, IGremlinQueryBase>[] unionTraversals) => Union<IGremlinQueryBase, IValueGremlinQuery<object>>(unionTraversals);

        IGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IGremlinQuery<TElement>>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);
        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Where(ILambda lambda) => Where(lambda);
        IGremlinQuery<TElement> IGremlinQueryBaseRec<IGremlinQuery<TElement>>.Where(Func<IGremlinQuery<TElement>, IGremlinQueryBase> filterTraversal) => Where(filterTraversal);

        TTargetQuery IGremlinQueryBaseRec<TElement, IValueGremlinQuery<TElement>>.Aggregate<TTargetQuery>(Func<IValueGremlinQuery<TElement>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IValueGremlinQuery<TElement>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Global, continuation);
        TTargetQuery IGremlinQueryBaseRec<TElement, IValueGremlinQuery<TElement>>.AggregateLocal<TTargetQuery>(Func<IValueGremlinQuery<TElement>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IValueGremlinQuery<TElement>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Local, continuation);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IValueGremlinQuery<TElement>>.Aggregate(StepLabel<IArrayGremlinQuery<TElement[], TElement, IValueGremlinQuery<TElement>>, TElement[]> stepLabel) => Aggregate(Scope.Global, stepLabel);
        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IValueGremlinQuery<TElement>>.AggregateLocal(StepLabel<IArrayGremlinQuery<TElement[], TElement, IValueGremlinQuery<TElement>>, TElement[]> stepLabel) => Aggregate(Scope.Local, stepLabel);

        TTargetQuery IGremlinQueryBaseRec<TElement, IValueGremlinQuery<TElement>>.As<TTargetQuery>(Func<IValueGremlinQuery<TElement>, StepLabel<IValueGremlinQuery<TElement>, TElement>, TTargetQuery> continuation) => As<StepLabel<IValueGremlinQuery<TElement>, TElement>, TTargetQuery>(continuation);
        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IValueGremlinQuery<TElement>>.As(StepLabel<TElement> stepLabel) => As(stepLabel);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.And(params Func<IValueGremlinQuery<TElement>, IGremlinQueryBase>[] andTraversals) => And(andTraversals);

        TTargetQuery IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.As<TTargetQuery>(Func<IValueGremlinQuery<TElement>, StepLabel<IValueGremlinQuery<TElement>, object>, TTargetQuery> continuation) => As<StepLabel<IValueGremlinQuery<TElement>, object>, TTargetQuery>(continuation);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Barrier() => Barrier();

        TTargetQuery IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Choose<TTargetQuery>(Func<IValueGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IValueGremlinQuery<TElement>, TTargetQuery> trueChoice, Func<IValueGremlinQuery<TElement>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(traversalPredicate, trueChoice, falseChoice);
        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Choose(Func<IValueGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IValueGremlinQuery<TElement>, IValueGremlinQuery<TElement>> trueChoice) => Choose<IValueGremlinQuery<TElement>, IValueGremlinQuery<TElement>, IValueGremlinQuery<TElement>>(traversalPredicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Choose(Func<IValueGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IValueGremlinQuery<TElement>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(traversalPredicate, trueChoice);
        
        TTargetQuery IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Choose<TTargetQuery>(Func<IChooseBuilder<IValueGremlinQuery<TElement>>, IChooseBuilderWithCaseOrDefault<TTargetQuery>> continuation) => Choose<TTargetQuery>(continuation);

        TTargetQuery IGremlinQueryBaseRec<TElement, IValueGremlinQuery<TElement>>.Choose<TTargetQuery>(Expression<Func<TElement, bool>> predicate, Func<IValueGremlinQuery<TElement>, TTargetQuery> trueChoice, Func<IValueGremlinQuery<TElement>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(predicate, trueChoice, falseChoice);
        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IValueGremlinQuery<TElement>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IValueGremlinQuery<TElement>, IValueGremlinQuery<TElement>> trueChoice) => Choose<IValueGremlinQuery<TElement>, IValueGremlinQuery<TElement>, IValueGremlinQuery<TElement>>(predicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<TElement, IValueGremlinQuery<TElement>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IValueGremlinQuery<TElement>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(predicate, trueChoice);

        TTargetQuery IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Coalesce<TTargetQuery>(params Func<IValueGremlinQuery<TElement>, TTargetQuery>[] traversals) => Coalesce<TTargetQuery, TTargetQuery>(traversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Coalesce(params Func<IValueGremlinQuery<TElement>, IGremlinQueryBase>[] traversals) => Coalesce<IGremlinQueryBase, IValueGremlinQuery<object>>(traversals);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Coin(double probability) => Coin(probability);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.CyclicPath() => CyclicPath();

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Dedup() => DedupGlobal();

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.DedupLocal() => DedupLocal();

        TTargetQuery IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.FlatMap<TTargetQuery>(Func<IValueGremlinQuery<TElement>, TTargetQuery> mapping) => FlatMap(mapping);

        IArrayGremlinQuery<TElement[], TElement, IValueGremlinQuery<TElement>> IGremlinQueryBaseRec<TElement, IValueGremlinQuery<TElement>>.ForceArray() => CloneAs<IArrayGremlinQuery<TElement[], TElement, IValueGremlinQuery<TElement>>>();

        IArrayGremlinQuery<TElement[], TElement, IValueGremlinQuery<TElement>> IGremlinQueryBaseRec<TElement, IValueGremlinQuery<TElement>>.Fold() => Fold<IValueGremlinQuery<TElement>>();

        IValueGremlinQuery<IDictionary<TNewKey, TNewValue>> IGremlinQueryBaseRec<TElement, IValueGremlinQuery<TElement>>.Group<TNewKey, TNewValue>(Func<IGroupBuilder<TElement, IValueGremlinQuery<TElement>>, IGroupBuilderWithKeyAndValue<TNewKey, TNewValue>> groupBuilder) => Group(groupBuilder);
        IValueGremlinQuery<IDictionary<TNewKey, TElement[]>> IGremlinQueryBaseRec<TElement, IValueGremlinQuery<TElement>>.Group<TNewKey>(Func<IGroupBuilder<TElement, IValueGremlinQuery<TElement>>, IGroupBuilderWithKey<TElement, IValueGremlinQuery<TElement>, TNewKey>> groupBuilder) => Group(groupBuilder);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Identity() => Identity();

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IValueGremlinQuery<TElement>>.Inject(params TElement[] elements) => Inject(elements);
        
        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Limit(long count) => LimitGlobal(count);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.LimitLocal(long count) => LimitLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Local<TTargetQuery>(Func<IValueGremlinQuery<TElement> , TTargetQuery> localTraversal) => Local(localTraversal);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Loop(Func<IStartLoopBuilder<IValueGremlinQuery<TElement>>, IFinalLoopBuilder<IValueGremlinQuery<TElement>>> loopBuilderTransformation) => Loop(loopBuilderTransformation);

        TTargetQuery IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Map<TTargetQuery>(Func<IValueGremlinQuery<TElement>, TTargetQuery> mapping) => Map(mapping);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Mute() => Mute();

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Not(Func<IValueGremlinQuery<TElement>, IGremlinQueryBase> notTraversal) => Not(static (__, notTraversal) => notTraversal(__), notTraversal);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.None() => None();

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Optional(Func<IValueGremlinQuery<TElement>, IValueGremlinQuery<TElement>> optionalTraversal) => Optional(optionalTraversal);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Or(params Func<IValueGremlinQuery<TElement>, IGremlinQueryBase>[] orTraversals) => Or(orTraversals);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IValueGremlinQuery<TElement>>.Order(Func<IOrderBuilder<TElement, IValueGremlinQuery<TElement>>, IOrderBuilderWithBy<TElement, IValueGremlinQuery<TElement>>> projection) => OrderGlobal(projection);
        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IValueGremlinQuery<TElement>>.OrderLocal(Func<IOrderBuilder<TElement, IValueGremlinQuery<TElement>>, IOrderBuilderWithBy<TElement, IValueGremlinQuery<TElement>>> projection) => OrderLocal(projection);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Order(Func<IOrderBuilder<IValueGremlinQuery<TElement>>, IOrderBuilderWithBy<IValueGremlinQuery<TElement>>> projection) => OrderGlobal(projection);
        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.OrderLocal(Func<IOrderBuilder<IValueGremlinQuery<TElement>>, IOrderBuilderWithBy<IValueGremlinQuery<TElement>>> projection) => OrderLocal(projection);

        IValueGremlinQuery<dynamic> IGremlinQueryBaseRec<TElement, IValueGremlinQuery<TElement>>.Project(Func<IProjectBuilder<IValueGremlinQuery<TElement>, TElement>, IProjectDynamicResult> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IValueGremlinQuery<TElement>>.Project<TResult>(Func<IProjectBuilder<IValueGremlinQuery<TElement>, TElement>, IProjectMapResult<TResult>> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IValueGremlinQuery<TElement>>.Project<TResult>(Func<IProjectBuilder<IValueGremlinQuery<TElement>, TElement>, IProjectTupleResult<TResult>> continuation) => Project<TResult>(continuation);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Range(long low, long high) => RangeGlobal(low, high);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.RangeLocal(long low, long high) => RangeLocal(low, high);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.SideEffect(Func<IValueGremlinQuery<TElement>, IGremlinQueryBase> sideEffectTraversal) => SideEffect(sideEffectTraversal);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.SimplePath() => SimplePath();

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Skip(long count) => Skip(count, Scope.Global);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.SkipLocal(long count) => Skip(count, Scope.Local);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Tail(long count) => TailGlobal(count);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.TailLocal(long count) => TailLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Union<TTargetQuery>(params Func<IValueGremlinQuery<TElement>, TTargetQuery>[] unionTraversals) => Union<TTargetQuery, TTargetQuery>(unionTraversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Union(params Func<IValueGremlinQuery<TElement>, IGremlinQueryBase>[] unionTraversals) => Union<IGremlinQueryBase, IValueGremlinQuery<object>>(unionTraversals);

        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IValueGremlinQuery<TElement>>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);
        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Where(ILambda lambda) => Where(lambda);
        IValueGremlinQuery<TElement> IGremlinQueryBaseRec<IValueGremlinQuery<TElement>>.Where(Func<IValueGremlinQuery<TElement>, IGremlinQueryBase> filterTraversal) => Where(filterTraversal);

        TTargetQuery IGremlinQueryBaseRec<TElement, IMapGremlinQuery<TElement>>.Aggregate<TTargetQuery>(Func<IMapGremlinQuery<TElement>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IMapGremlinQuery<TElement>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Global, continuation);
        TTargetQuery IGremlinQueryBaseRec<TElement, IMapGremlinQuery<TElement>>.AggregateLocal<TTargetQuery>(Func<IMapGremlinQuery<TElement>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IMapGremlinQuery<TElement>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Local, continuation);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IMapGremlinQuery<TElement>>.Aggregate(StepLabel<IArrayGremlinQuery<TElement[], TElement, IMapGremlinQuery<TElement>>, TElement[]> stepLabel) => Aggregate(Scope.Global, stepLabel);
        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IMapGremlinQuery<TElement>>.AggregateLocal(StepLabel<IArrayGremlinQuery<TElement[], TElement, IMapGremlinQuery<TElement>>, TElement[]> stepLabel) => Aggregate(Scope.Local, stepLabel);

        TTargetQuery IGremlinQueryBaseRec<TElement, IMapGremlinQuery<TElement>>.As<TTargetQuery>(Func<IMapGremlinQuery<TElement>, StepLabel<IMapGremlinQuery<TElement>, TElement>, TTargetQuery> continuation) => As<StepLabel<IMapGremlinQuery<TElement>, TElement>, TTargetQuery>(continuation);
        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IMapGremlinQuery<TElement>>.As(StepLabel<TElement> stepLabel) => As(stepLabel);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.And(params Func<IMapGremlinQuery<TElement>, IGremlinQueryBase>[] andTraversals) => And(andTraversals);

        TTargetQuery IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.As<TTargetQuery>(Func<IMapGremlinQuery<TElement>, StepLabel<IMapGremlinQuery<TElement>, object>, TTargetQuery> continuation) => As<StepLabel<IMapGremlinQuery<TElement>, object>, TTargetQuery>(continuation);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Barrier() => Barrier();

        TTargetQuery IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Choose<TTargetQuery>(Func<IMapGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IMapGremlinQuery<TElement>, TTargetQuery> trueChoice, Func<IMapGremlinQuery<TElement>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(traversalPredicate, trueChoice, falseChoice);
        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Choose(Func<IMapGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IMapGremlinQuery<TElement>, IMapGremlinQuery<TElement>> trueChoice) => Choose<IMapGremlinQuery<TElement>, IMapGremlinQuery<TElement>, IMapGremlinQuery<TElement>>(traversalPredicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Choose(Func<IMapGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IMapGremlinQuery<TElement>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(traversalPredicate, trueChoice);
        
        TTargetQuery IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Choose<TTargetQuery>(Func<IChooseBuilder<IMapGremlinQuery<TElement>>, IChooseBuilderWithCaseOrDefault<TTargetQuery>> continuation) => Choose<TTargetQuery>(continuation);

        TTargetQuery IGremlinQueryBaseRec<TElement, IMapGremlinQuery<TElement>>.Choose<TTargetQuery>(Expression<Func<TElement, bool>> predicate, Func<IMapGremlinQuery<TElement>, TTargetQuery> trueChoice, Func<IMapGremlinQuery<TElement>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(predicate, trueChoice, falseChoice);
        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IMapGremlinQuery<TElement>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IMapGremlinQuery<TElement>, IMapGremlinQuery<TElement>> trueChoice) => Choose<IMapGremlinQuery<TElement>, IMapGremlinQuery<TElement>, IMapGremlinQuery<TElement>>(predicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<TElement, IMapGremlinQuery<TElement>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IMapGremlinQuery<TElement>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(predicate, trueChoice);

        TTargetQuery IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Coalesce<TTargetQuery>(params Func<IMapGremlinQuery<TElement>, TTargetQuery>[] traversals) => Coalesce<TTargetQuery, TTargetQuery>(traversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Coalesce(params Func<IMapGremlinQuery<TElement>, IGremlinQueryBase>[] traversals) => Coalesce<IGremlinQueryBase, IValueGremlinQuery<object>>(traversals);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Coin(double probability) => Coin(probability);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.CyclicPath() => CyclicPath();

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Dedup() => DedupGlobal();

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.DedupLocal() => DedupLocal();

        TTargetQuery IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.FlatMap<TTargetQuery>(Func<IMapGremlinQuery<TElement>, TTargetQuery> mapping) => FlatMap(mapping);

        IArrayGremlinQuery<TElement[], TElement, IMapGremlinQuery<TElement>> IGremlinQueryBaseRec<TElement, IMapGremlinQuery<TElement>>.ForceArray() => CloneAs<IArrayGremlinQuery<TElement[], TElement, IMapGremlinQuery<TElement>>>();

        IArrayGremlinQuery<TElement[], TElement, IMapGremlinQuery<TElement>> IGremlinQueryBaseRec<TElement, IMapGremlinQuery<TElement>>.Fold() => Fold<IMapGremlinQuery<TElement>>();

        IValueGremlinQuery<IDictionary<TNewKey, TNewValue>> IGremlinQueryBaseRec<TElement, IMapGremlinQuery<TElement>>.Group<TNewKey, TNewValue>(Func<IGroupBuilder<TElement, IMapGremlinQuery<TElement>>, IGroupBuilderWithKeyAndValue<TNewKey, TNewValue>> groupBuilder) => Group(groupBuilder);
        IValueGremlinQuery<IDictionary<TNewKey, TElement[]>> IGremlinQueryBaseRec<TElement, IMapGremlinQuery<TElement>>.Group<TNewKey>(Func<IGroupBuilder<TElement, IMapGremlinQuery<TElement>>, IGroupBuilderWithKey<TElement, IMapGremlinQuery<TElement>, TNewKey>> groupBuilder) => Group(groupBuilder);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Identity() => Identity();

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IMapGremlinQuery<TElement>>.Inject(params TElement[] elements) => Inject(elements);
        
        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Limit(long count) => LimitGlobal(count);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.LimitLocal(long count) => LimitLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Local<TTargetQuery>(Func<IMapGremlinQuery<TElement> , TTargetQuery> localTraversal) => Local(localTraversal);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Loop(Func<IStartLoopBuilder<IMapGremlinQuery<TElement>>, IFinalLoopBuilder<IMapGremlinQuery<TElement>>> loopBuilderTransformation) => Loop(loopBuilderTransformation);

        TTargetQuery IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Map<TTargetQuery>(Func<IMapGremlinQuery<TElement>, TTargetQuery> mapping) => Map(mapping);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Mute() => Mute();

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Not(Func<IMapGremlinQuery<TElement>, IGremlinQueryBase> notTraversal) => Not(static (__, notTraversal) => notTraversal(__), notTraversal);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.None() => None();

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Optional(Func<IMapGremlinQuery<TElement>, IMapGremlinQuery<TElement>> optionalTraversal) => Optional(optionalTraversal);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Or(params Func<IMapGremlinQuery<TElement>, IGremlinQueryBase>[] orTraversals) => Or(orTraversals);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IMapGremlinQuery<TElement>>.Order(Func<IOrderBuilder<TElement, IMapGremlinQuery<TElement>>, IOrderBuilderWithBy<TElement, IMapGremlinQuery<TElement>>> projection) => OrderGlobal(projection);
        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IMapGremlinQuery<TElement>>.OrderLocal(Func<IOrderBuilder<TElement, IMapGremlinQuery<TElement>>, IOrderBuilderWithBy<TElement, IMapGremlinQuery<TElement>>> projection) => OrderLocal(projection);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Order(Func<IOrderBuilder<IMapGremlinQuery<TElement>>, IOrderBuilderWithBy<IMapGremlinQuery<TElement>>> projection) => OrderGlobal(projection);
        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.OrderLocal(Func<IOrderBuilder<IMapGremlinQuery<TElement>>, IOrderBuilderWithBy<IMapGremlinQuery<TElement>>> projection) => OrderLocal(projection);

        IValueGremlinQuery<dynamic> IGremlinQueryBaseRec<TElement, IMapGremlinQuery<TElement>>.Project(Func<IProjectBuilder<IMapGremlinQuery<TElement>, TElement>, IProjectDynamicResult> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IMapGremlinQuery<TElement>>.Project<TResult>(Func<IProjectBuilder<IMapGremlinQuery<TElement>, TElement>, IProjectMapResult<TResult>> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IMapGremlinQuery<TElement>>.Project<TResult>(Func<IProjectBuilder<IMapGremlinQuery<TElement>, TElement>, IProjectTupleResult<TResult>> continuation) => Project<TResult>(continuation);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Range(long low, long high) => RangeGlobal(low, high);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.RangeLocal(long low, long high) => RangeLocal(low, high);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.SideEffect(Func<IMapGremlinQuery<TElement>, IGremlinQueryBase> sideEffectTraversal) => SideEffect(sideEffectTraversal);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.SimplePath() => SimplePath();

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Skip(long count) => Skip(count, Scope.Global);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.SkipLocal(long count) => Skip(count, Scope.Local);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Tail(long count) => TailGlobal(count);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.TailLocal(long count) => TailLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Union<TTargetQuery>(params Func<IMapGremlinQuery<TElement>, TTargetQuery>[] unionTraversals) => Union<TTargetQuery, TTargetQuery>(unionTraversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Union(params Func<IMapGremlinQuery<TElement>, IGremlinQueryBase>[] unionTraversals) => Union<IGremlinQueryBase, IValueGremlinQuery<object>>(unionTraversals);

        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IMapGremlinQuery<TElement>>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);
        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Where(ILambda lambda) => Where(lambda);
        IMapGremlinQuery<TElement> IGremlinQueryBaseRec<IMapGremlinQuery<TElement>>.Where(Func<IMapGremlinQuery<TElement>, IGremlinQueryBase> filterTraversal) => Where(filterTraversal);

        TTargetQuery IGremlinQueryBaseRec<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Aggregate<TTargetQuery>(Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Global, continuation);
        TTargetQuery IGremlinQueryBaseRec<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.AggregateLocal<TTargetQuery>(Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Local, continuation);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Aggregate(StepLabel<IArrayGremlinQuery<TElement[], TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>, TElement[]> stepLabel) => Aggregate(Scope.Global, stepLabel);
        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.AggregateLocal(StepLabel<IArrayGremlinQuery<TElement[], TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>, TElement[]> stepLabel) => Aggregate(Scope.Local, stepLabel);

        TTargetQuery IGremlinQueryBaseRec<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.As<TTargetQuery>(Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, StepLabel<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, TElement>, TTargetQuery> continuation) => As<StepLabel<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, TElement>, TTargetQuery>(continuation);
        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.As(StepLabel<TElement> stepLabel) => As(stepLabel);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.And(params Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, IGremlinQueryBase>[] andTraversals) => And(andTraversals);

        TTargetQuery IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.As<TTargetQuery>(Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, StepLabel<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, object>, TTargetQuery> continuation) => As<StepLabel<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, object>, TTargetQuery>(continuation);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Barrier() => Barrier();

        TTargetQuery IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Choose<TTargetQuery>(Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, IGremlinQueryBase> traversalPredicate, Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, TTargetQuery> trueChoice, Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(traversalPredicate, trueChoice, falseChoice);
        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Choose(Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, IGremlinQueryBase> traversalPredicate, Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>> trueChoice) => Choose<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>(traversalPredicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Choose(Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, IGremlinQueryBase> traversalPredicate, Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(traversalPredicate, trueChoice);
        
        TTargetQuery IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Choose<TTargetQuery>(Func<IChooseBuilder<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>, IChooseBuilderWithCaseOrDefault<TTargetQuery>> continuation) => Choose<TTargetQuery>(continuation);

        TTargetQuery IGremlinQueryBaseRec<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Choose<TTargetQuery>(Expression<Func<TElement, bool>> predicate, Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, TTargetQuery> trueChoice, Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(predicate, trueChoice, falseChoice);
        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>> trueChoice) => Choose<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>(predicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(predicate, trueChoice);

        TTargetQuery IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Coalesce<TTargetQuery>(params Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, TTargetQuery>[] traversals) => Coalesce<TTargetQuery, TTargetQuery>(traversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Coalesce(params Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, IGremlinQueryBase>[] traversals) => Coalesce<IGremlinQueryBase, IValueGremlinQuery<object>>(traversals);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Coin(double probability) => Coin(probability);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.CyclicPath() => CyclicPath();

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Dedup() => DedupGlobal();

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.DedupLocal() => DedupLocal();

        TTargetQuery IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.FlatMap<TTargetQuery>(Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, TTargetQuery> mapping) => FlatMap(mapping);

        IArrayGremlinQuery<TElement[], TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>> IGremlinQueryBaseRec<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.ForceArray() => CloneAs<IArrayGremlinQuery<TElement[], TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>>();

        IArrayGremlinQuery<TElement[], TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>> IGremlinQueryBaseRec<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Fold() => Fold<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>();

        IValueGremlinQuery<IDictionary<TNewKey, TNewValue>> IGremlinQueryBaseRec<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Group<TNewKey, TNewValue>(Func<IGroupBuilder<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>, IGroupBuilderWithKeyAndValue<TNewKey, TNewValue>> groupBuilder) => Group(groupBuilder);
        IValueGremlinQuery<IDictionary<TNewKey, TElement[]>> IGremlinQueryBaseRec<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Group<TNewKey>(Func<IGroupBuilder<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>, IGroupBuilderWithKey<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, TNewKey>> groupBuilder) => Group(groupBuilder);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Identity() => Identity();

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Inject(params TElement[] elements) => Inject(elements);
        
        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Limit(long count) => LimitGlobal(count);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.LimitLocal(long count) => LimitLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Local<TTargetQuery>(Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> , TTargetQuery> localTraversal) => Local(localTraversal);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Loop(Func<IStartLoopBuilder<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>, IFinalLoopBuilder<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>> loopBuilderTransformation) => Loop(loopBuilderTransformation);

        TTargetQuery IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Map<TTargetQuery>(Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, TTargetQuery> mapping) => Map(mapping);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Mute() => Mute();

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Not(Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, IGremlinQueryBase> notTraversal) => Not(static (__, notTraversal) => notTraversal(__), notTraversal);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.None() => None();

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Optional(Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>> optionalTraversal) => Optional(optionalTraversal);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Or(params Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, IGremlinQueryBase>[] orTraversals) => Or(orTraversals);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Order(Func<IOrderBuilder<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>, IOrderBuilderWithBy<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>> projection) => OrderGlobal(projection);
        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.OrderLocal(Func<IOrderBuilder<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>, IOrderBuilderWithBy<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>> projection) => OrderLocal(projection);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Order(Func<IOrderBuilder<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>, IOrderBuilderWithBy<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>> projection) => OrderGlobal(projection);
        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.OrderLocal(Func<IOrderBuilder<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>, IOrderBuilderWithBy<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>> projection) => OrderLocal(projection);

        IValueGremlinQuery<dynamic> IGremlinQueryBaseRec<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Project(Func<IProjectBuilder<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, TElement>, IProjectDynamicResult> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Project<TResult>(Func<IProjectBuilder<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, TElement>, IProjectMapResult<TResult>> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Project<TResult>(Func<IProjectBuilder<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, TElement>, IProjectTupleResult<TResult>> continuation) => Project<TResult>(continuation);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Range(long low, long high) => RangeGlobal(low, high);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.RangeLocal(long low, long high) => RangeLocal(low, high);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.SideEffect(Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, IGremlinQueryBase> sideEffectTraversal) => SideEffect(sideEffectTraversal);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.SimplePath() => SimplePath();

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Skip(long count) => Skip(count, Scope.Global);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.SkipLocal(long count) => Skip(count, Scope.Local);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Tail(long count) => TailGlobal(count);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.TailLocal(long count) => TailLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Union<TTargetQuery>(params Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, TTargetQuery>[] unionTraversals) => Union<TTargetQuery, TTargetQuery>(unionTraversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Union(params Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, IGremlinQueryBase>[] unionTraversals) => Union<IGremlinQueryBase, IValueGremlinQuery<object>>(unionTraversals);

        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<TElement, IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);
        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Where(ILambda lambda) => Where(lambda);
        IArrayGremlinQuery<TElement, TScalar, TFoldedQuery> IGremlinQueryBaseRec<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>>.Where(Func<IArrayGremlinQuery<TElement, TScalar, TFoldedQuery>, IGremlinQueryBase> filterTraversal) => Where(filterTraversal);

        TTargetQuery IGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.Aggregate<TTargetQuery>(Func<IElementGremlinQuery<TElement>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IElementGremlinQuery<TElement>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Global, continuation);
        TTargetQuery IGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.AggregateLocal<TTargetQuery>(Func<IElementGremlinQuery<TElement>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IElementGremlinQuery<TElement>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Local, continuation);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.Aggregate(StepLabel<IArrayGremlinQuery<TElement[], TElement, IElementGremlinQuery<TElement>>, TElement[]> stepLabel) => Aggregate(Scope.Global, stepLabel);
        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.AggregateLocal(StepLabel<IArrayGremlinQuery<TElement[], TElement, IElementGremlinQuery<TElement>>, TElement[]> stepLabel) => Aggregate(Scope.Local, stepLabel);

        TTargetQuery IGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.As<TTargetQuery>(Func<IElementGremlinQuery<TElement>, StepLabel<IElementGremlinQuery<TElement>, TElement>, TTargetQuery> continuation) => As<StepLabel<IElementGremlinQuery<TElement>, TElement>, TTargetQuery>(continuation);
        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.As(StepLabel<TElement> stepLabel) => As(stepLabel);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.And(params Func<IElementGremlinQuery<TElement>, IGremlinQueryBase>[] andTraversals) => And(andTraversals);

        TTargetQuery IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.As<TTargetQuery>(Func<IElementGremlinQuery<TElement>, StepLabel<IElementGremlinQuery<TElement>, object>, TTargetQuery> continuation) => As<StepLabel<IElementGremlinQuery<TElement>, object>, TTargetQuery>(continuation);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Barrier() => Barrier();

        TTargetQuery IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Choose<TTargetQuery>(Func<IElementGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IElementGremlinQuery<TElement>, TTargetQuery> trueChoice, Func<IElementGremlinQuery<TElement>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(traversalPredicate, trueChoice, falseChoice);
        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Choose(Func<IElementGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IElementGremlinQuery<TElement>, IElementGremlinQuery<TElement>> trueChoice) => Choose<IElementGremlinQuery<TElement>, IElementGremlinQuery<TElement>, IElementGremlinQuery<TElement>>(traversalPredicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Choose(Func<IElementGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IElementGremlinQuery<TElement>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(traversalPredicate, trueChoice);
        
        TTargetQuery IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Choose<TTargetQuery>(Func<IChooseBuilder<IElementGremlinQuery<TElement>>, IChooseBuilderWithCaseOrDefault<TTargetQuery>> continuation) => Choose<TTargetQuery>(continuation);

        TTargetQuery IGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.Choose<TTargetQuery>(Expression<Func<TElement, bool>> predicate, Func<IElementGremlinQuery<TElement>, TTargetQuery> trueChoice, Func<IElementGremlinQuery<TElement>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(predicate, trueChoice, falseChoice);
        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IElementGremlinQuery<TElement>, IElementGremlinQuery<TElement>> trueChoice) => Choose<IElementGremlinQuery<TElement>, IElementGremlinQuery<TElement>, IElementGremlinQuery<TElement>>(predicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IElementGremlinQuery<TElement>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(predicate, trueChoice);

        TTargetQuery IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Coalesce<TTargetQuery>(params Func<IElementGremlinQuery<TElement>, TTargetQuery>[] traversals) => Coalesce<TTargetQuery, TTargetQuery>(traversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Coalesce(params Func<IElementGremlinQuery<TElement>, IGremlinQueryBase>[] traversals) => Coalesce<IGremlinQueryBase, IValueGremlinQuery<object>>(traversals);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Coin(double probability) => Coin(probability);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.CyclicPath() => CyclicPath();

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Dedup() => DedupGlobal();

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.DedupLocal() => DedupLocal();

        TTargetQuery IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.FlatMap<TTargetQuery>(Func<IElementGremlinQuery<TElement>, TTargetQuery> mapping) => FlatMap(mapping);

        IArrayGremlinQuery<TElement[], TElement, IElementGremlinQuery<TElement>> IGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.ForceArray() => CloneAs<IArrayGremlinQuery<TElement[], TElement, IElementGremlinQuery<TElement>>>();

        IArrayGremlinQuery<TElement[], TElement, IElementGremlinQuery<TElement>> IGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.Fold() => Fold<IElementGremlinQuery<TElement>>();

        IValueGremlinQuery<IDictionary<TNewKey, TNewValue>> IGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.Group<TNewKey, TNewValue>(Func<IGroupBuilder<TElement, IElementGremlinQuery<TElement>>, IGroupBuilderWithKeyAndValue<TNewKey, TNewValue>> groupBuilder) => Group(groupBuilder);
        IValueGremlinQuery<IDictionary<TNewKey, TElement[]>> IGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.Group<TNewKey>(Func<IGroupBuilder<TElement, IElementGremlinQuery<TElement>>, IGroupBuilderWithKey<TElement, IElementGremlinQuery<TElement>, TNewKey>> groupBuilder) => Group(groupBuilder);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Identity() => Identity();

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.Inject(params TElement[] elements) => Inject(elements);
        
        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Limit(long count) => LimitGlobal(count);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.LimitLocal(long count) => LimitLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Local<TTargetQuery>(Func<IElementGremlinQuery<TElement> , TTargetQuery> localTraversal) => Local(localTraversal);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Loop(Func<IStartLoopBuilder<IElementGremlinQuery<TElement>>, IFinalLoopBuilder<IElementGremlinQuery<TElement>>> loopBuilderTransformation) => Loop(loopBuilderTransformation);

        TTargetQuery IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Map<TTargetQuery>(Func<IElementGremlinQuery<TElement>, TTargetQuery> mapping) => Map(mapping);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Mute() => Mute();

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Not(Func<IElementGremlinQuery<TElement>, IGremlinQueryBase> notTraversal) => Not(static (__, notTraversal) => notTraversal(__), notTraversal);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.None() => None();

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Optional(Func<IElementGremlinQuery<TElement>, IElementGremlinQuery<TElement>> optionalTraversal) => Optional(optionalTraversal);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Or(params Func<IElementGremlinQuery<TElement>, IGremlinQueryBase>[] orTraversals) => Or(orTraversals);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.Order(Func<IOrderBuilder<TElement, IElementGremlinQuery<TElement>>, IOrderBuilderWithBy<TElement, IElementGremlinQuery<TElement>>> projection) => OrderGlobal(projection);
        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.OrderLocal(Func<IOrderBuilder<TElement, IElementGremlinQuery<TElement>>, IOrderBuilderWithBy<TElement, IElementGremlinQuery<TElement>>> projection) => OrderLocal(projection);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Order(Func<IOrderBuilder<IElementGremlinQuery<TElement>>, IOrderBuilderWithBy<IElementGremlinQuery<TElement>>> projection) => OrderGlobal(projection);
        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.OrderLocal(Func<IOrderBuilder<IElementGremlinQuery<TElement>>, IOrderBuilderWithBy<IElementGremlinQuery<TElement>>> projection) => OrderLocal(projection);

        IValueGremlinQuery<dynamic> IGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.Project(Func<IProjectBuilder<IElementGremlinQuery<TElement>, TElement>, IProjectDynamicResult> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.Project<TResult>(Func<IProjectBuilder<IElementGremlinQuery<TElement>, TElement>, IProjectMapResult<TResult>> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.Project<TResult>(Func<IProjectBuilder<IElementGremlinQuery<TElement>, TElement>, IProjectTupleResult<TResult>> continuation) => Project<TResult>(continuation);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Range(long low, long high) => RangeGlobal(low, high);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.RangeLocal(long low, long high) => RangeLocal(low, high);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.SideEffect(Func<IElementGremlinQuery<TElement>, IGremlinQueryBase> sideEffectTraversal) => SideEffect(sideEffectTraversal);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.SimplePath() => SimplePath();

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Skip(long count) => Skip(count, Scope.Global);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.SkipLocal(long count) => Skip(count, Scope.Local);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Tail(long count) => TailGlobal(count);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.TailLocal(long count) => TailLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Union<TTargetQuery>(params Func<IElementGremlinQuery<TElement>, TTargetQuery>[] unionTraversals) => Union<TTargetQuery, TTargetQuery>(unionTraversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Union(params Func<IElementGremlinQuery<TElement>, IGremlinQueryBase>[] unionTraversals) => Union<IGremlinQueryBase, IValueGremlinQuery<object>>(unionTraversals);

        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);
        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Where(ILambda lambda) => Where(lambda);
        IElementGremlinQuery<TElement> IGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Where(Func<IElementGremlinQuery<TElement>, IGremlinQueryBase> filterTraversal) => Where(filterTraversal);

        TTargetQuery IGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.Aggregate<TTargetQuery>(Func<IEdgeOrVertexGremlinQuery<TElement>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IEdgeOrVertexGremlinQuery<TElement>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Global, continuation);
        TTargetQuery IGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.AggregateLocal<TTargetQuery>(Func<IEdgeOrVertexGremlinQuery<TElement>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IEdgeOrVertexGremlinQuery<TElement>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Local, continuation);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.Aggregate(StepLabel<IArrayGremlinQuery<TElement[], TElement, IEdgeOrVertexGremlinQuery<TElement>>, TElement[]> stepLabel) => Aggregate(Scope.Global, stepLabel);
        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.AggregateLocal(StepLabel<IArrayGremlinQuery<TElement[], TElement, IEdgeOrVertexGremlinQuery<TElement>>, TElement[]> stepLabel) => Aggregate(Scope.Local, stepLabel);

        TTargetQuery IGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.As<TTargetQuery>(Func<IEdgeOrVertexGremlinQuery<TElement>, StepLabel<IEdgeOrVertexGremlinQuery<TElement>, TElement>, TTargetQuery> continuation) => As<StepLabel<IEdgeOrVertexGremlinQuery<TElement>, TElement>, TTargetQuery>(continuation);
        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.As(StepLabel<TElement> stepLabel) => As(stepLabel);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.And(params Func<IEdgeOrVertexGremlinQuery<TElement>, IGremlinQueryBase>[] andTraversals) => And(andTraversals);

        TTargetQuery IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.As<TTargetQuery>(Func<IEdgeOrVertexGremlinQuery<TElement>, StepLabel<IEdgeOrVertexGremlinQuery<TElement>, object>, TTargetQuery> continuation) => As<StepLabel<IEdgeOrVertexGremlinQuery<TElement>, object>, TTargetQuery>(continuation);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Barrier() => Barrier();

        TTargetQuery IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Choose<TTargetQuery>(Func<IEdgeOrVertexGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IEdgeOrVertexGremlinQuery<TElement>, TTargetQuery> trueChoice, Func<IEdgeOrVertexGremlinQuery<TElement>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(traversalPredicate, trueChoice, falseChoice);
        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Choose(Func<IEdgeOrVertexGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IEdgeOrVertexGremlinQuery<TElement>, IEdgeOrVertexGremlinQuery<TElement>> trueChoice) => Choose<IEdgeOrVertexGremlinQuery<TElement>, IEdgeOrVertexGremlinQuery<TElement>, IEdgeOrVertexGremlinQuery<TElement>>(traversalPredicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Choose(Func<IEdgeOrVertexGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IEdgeOrVertexGremlinQuery<TElement>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(traversalPredicate, trueChoice);
        
        TTargetQuery IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Choose<TTargetQuery>(Func<IChooseBuilder<IEdgeOrVertexGremlinQuery<TElement>>, IChooseBuilderWithCaseOrDefault<TTargetQuery>> continuation) => Choose<TTargetQuery>(continuation);

        TTargetQuery IGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.Choose<TTargetQuery>(Expression<Func<TElement, bool>> predicate, Func<IEdgeOrVertexGremlinQuery<TElement>, TTargetQuery> trueChoice, Func<IEdgeOrVertexGremlinQuery<TElement>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(predicate, trueChoice, falseChoice);
        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IEdgeOrVertexGremlinQuery<TElement>, IEdgeOrVertexGremlinQuery<TElement>> trueChoice) => Choose<IEdgeOrVertexGremlinQuery<TElement>, IEdgeOrVertexGremlinQuery<TElement>, IEdgeOrVertexGremlinQuery<TElement>>(predicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IEdgeOrVertexGremlinQuery<TElement>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(predicate, trueChoice);

        TTargetQuery IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Coalesce<TTargetQuery>(params Func<IEdgeOrVertexGremlinQuery<TElement>, TTargetQuery>[] traversals) => Coalesce<TTargetQuery, TTargetQuery>(traversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Coalesce(params Func<IEdgeOrVertexGremlinQuery<TElement>, IGremlinQueryBase>[] traversals) => Coalesce<IGremlinQueryBase, IValueGremlinQuery<object>>(traversals);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Coin(double probability) => Coin(probability);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.CyclicPath() => CyclicPath();

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Dedup() => DedupGlobal();

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.DedupLocal() => DedupLocal();

        TTargetQuery IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.FlatMap<TTargetQuery>(Func<IEdgeOrVertexGremlinQuery<TElement>, TTargetQuery> mapping) => FlatMap(mapping);

        IArrayGremlinQuery<TElement[], TElement, IEdgeOrVertexGremlinQuery<TElement>> IGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.ForceArray() => CloneAs<IArrayGremlinQuery<TElement[], TElement, IEdgeOrVertexGremlinQuery<TElement>>>();

        IArrayGremlinQuery<TElement[], TElement, IEdgeOrVertexGremlinQuery<TElement>> IGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.Fold() => Fold<IEdgeOrVertexGremlinQuery<TElement>>();

        IValueGremlinQuery<IDictionary<TNewKey, TNewValue>> IGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.Group<TNewKey, TNewValue>(Func<IGroupBuilder<TElement, IEdgeOrVertexGremlinQuery<TElement>>, IGroupBuilderWithKeyAndValue<TNewKey, TNewValue>> groupBuilder) => Group(groupBuilder);
        IValueGremlinQuery<IDictionary<TNewKey, TElement[]>> IGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.Group<TNewKey>(Func<IGroupBuilder<TElement, IEdgeOrVertexGremlinQuery<TElement>>, IGroupBuilderWithKey<TElement, IEdgeOrVertexGremlinQuery<TElement>, TNewKey>> groupBuilder) => Group(groupBuilder);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Identity() => Identity();

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.Inject(params TElement[] elements) => Inject(elements);
        
        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Limit(long count) => LimitGlobal(count);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.LimitLocal(long count) => LimitLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Local<TTargetQuery>(Func<IEdgeOrVertexGremlinQuery<TElement> , TTargetQuery> localTraversal) => Local(localTraversal);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Loop(Func<IStartLoopBuilder<IEdgeOrVertexGremlinQuery<TElement>>, IFinalLoopBuilder<IEdgeOrVertexGremlinQuery<TElement>>> loopBuilderTransformation) => Loop(loopBuilderTransformation);

        TTargetQuery IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Map<TTargetQuery>(Func<IEdgeOrVertexGremlinQuery<TElement>, TTargetQuery> mapping) => Map(mapping);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Mute() => Mute();

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Not(Func<IEdgeOrVertexGremlinQuery<TElement>, IGremlinQueryBase> notTraversal) => Not(static (__, notTraversal) => notTraversal(__), notTraversal);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.None() => None();

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Optional(Func<IEdgeOrVertexGremlinQuery<TElement>, IEdgeOrVertexGremlinQuery<TElement>> optionalTraversal) => Optional(optionalTraversal);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Or(params Func<IEdgeOrVertexGremlinQuery<TElement>, IGremlinQueryBase>[] orTraversals) => Or(orTraversals);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.Order(Func<IOrderBuilder<TElement, IEdgeOrVertexGremlinQuery<TElement>>, IOrderBuilderWithBy<TElement, IEdgeOrVertexGremlinQuery<TElement>>> projection) => OrderGlobal(projection);
        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.OrderLocal(Func<IOrderBuilder<TElement, IEdgeOrVertexGremlinQuery<TElement>>, IOrderBuilderWithBy<TElement, IEdgeOrVertexGremlinQuery<TElement>>> projection) => OrderLocal(projection);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Order(Func<IOrderBuilder<IEdgeOrVertexGremlinQuery<TElement>>, IOrderBuilderWithBy<IEdgeOrVertexGremlinQuery<TElement>>> projection) => OrderGlobal(projection);
        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.OrderLocal(Func<IOrderBuilder<IEdgeOrVertexGremlinQuery<TElement>>, IOrderBuilderWithBy<IEdgeOrVertexGremlinQuery<TElement>>> projection) => OrderLocal(projection);

        IValueGremlinQuery<dynamic> IGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.Project(Func<IProjectBuilder<IEdgeOrVertexGremlinQuery<TElement>, TElement>, IProjectDynamicResult> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.Project<TResult>(Func<IProjectBuilder<IEdgeOrVertexGremlinQuery<TElement>, TElement>, IProjectMapResult<TResult>> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.Project<TResult>(Func<IProjectBuilder<IEdgeOrVertexGremlinQuery<TElement>, TElement>, IProjectTupleResult<TResult>> continuation) => Project<TResult>(continuation);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Range(long low, long high) => RangeGlobal(low, high);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.RangeLocal(long low, long high) => RangeLocal(low, high);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.SideEffect(Func<IEdgeOrVertexGremlinQuery<TElement>, IGremlinQueryBase> sideEffectTraversal) => SideEffect(sideEffectTraversal);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.SimplePath() => SimplePath();

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Skip(long count) => Skip(count, Scope.Global);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.SkipLocal(long count) => Skip(count, Scope.Local);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Tail(long count) => TailGlobal(count);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.TailLocal(long count) => TailLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Union<TTargetQuery>(params Func<IEdgeOrVertexGremlinQuery<TElement>, TTargetQuery>[] unionTraversals) => Union<TTargetQuery, TTargetQuery>(unionTraversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Union(params Func<IEdgeOrVertexGremlinQuery<TElement>, IGremlinQueryBase>[] unionTraversals) => Union<IGremlinQueryBase, IValueGremlinQuery<object>>(unionTraversals);

        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);
        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Where(ILambda lambda) => Where(lambda);
        IEdgeOrVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Where(Func<IEdgeOrVertexGremlinQuery<TElement>, IGremlinQueryBase> filterTraversal) => Where(filterTraversal);

        TTargetQuery IGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.Aggregate<TTargetQuery>(Func<IVertexGremlinQuery<TElement>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IVertexGremlinQuery<TElement>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Global, continuation);
        TTargetQuery IGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.AggregateLocal<TTargetQuery>(Func<IVertexGremlinQuery<TElement>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IVertexGremlinQuery<TElement>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Local, continuation);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.Aggregate(StepLabel<IArrayGremlinQuery<TElement[], TElement, IVertexGremlinQuery<TElement>>, TElement[]> stepLabel) => Aggregate(Scope.Global, stepLabel);
        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.AggregateLocal(StepLabel<IArrayGremlinQuery<TElement[], TElement, IVertexGremlinQuery<TElement>>, TElement[]> stepLabel) => Aggregate(Scope.Local, stepLabel);

        TTargetQuery IGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.As<TTargetQuery>(Func<IVertexGremlinQuery<TElement>, StepLabel<IVertexGremlinQuery<TElement>, TElement>, TTargetQuery> continuation) => As<StepLabel<IVertexGremlinQuery<TElement>, TElement>, TTargetQuery>(continuation);
        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.As(StepLabel<TElement> stepLabel) => As(stepLabel);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.And(params Func<IVertexGremlinQuery<TElement>, IGremlinQueryBase>[] andTraversals) => And(andTraversals);

        TTargetQuery IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.As<TTargetQuery>(Func<IVertexGremlinQuery<TElement>, StepLabel<IVertexGremlinQuery<TElement>, object>, TTargetQuery> continuation) => As<StepLabel<IVertexGremlinQuery<TElement>, object>, TTargetQuery>(continuation);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Barrier() => Barrier();

        TTargetQuery IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Choose<TTargetQuery>(Func<IVertexGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IVertexGremlinQuery<TElement>, TTargetQuery> trueChoice, Func<IVertexGremlinQuery<TElement>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(traversalPredicate, trueChoice, falseChoice);
        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Choose(Func<IVertexGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IVertexGremlinQuery<TElement>, IVertexGremlinQuery<TElement>> trueChoice) => Choose<IVertexGremlinQuery<TElement>, IVertexGremlinQuery<TElement>, IVertexGremlinQuery<TElement>>(traversalPredicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Choose(Func<IVertexGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IVertexGremlinQuery<TElement>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(traversalPredicate, trueChoice);
        
        TTargetQuery IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Choose<TTargetQuery>(Func<IChooseBuilder<IVertexGremlinQuery<TElement>>, IChooseBuilderWithCaseOrDefault<TTargetQuery>> continuation) => Choose<TTargetQuery>(continuation);

        TTargetQuery IGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.Choose<TTargetQuery>(Expression<Func<TElement, bool>> predicate, Func<IVertexGremlinQuery<TElement>, TTargetQuery> trueChoice, Func<IVertexGremlinQuery<TElement>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(predicate, trueChoice, falseChoice);
        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IVertexGremlinQuery<TElement>, IVertexGremlinQuery<TElement>> trueChoice) => Choose<IVertexGremlinQuery<TElement>, IVertexGremlinQuery<TElement>, IVertexGremlinQuery<TElement>>(predicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IVertexGremlinQuery<TElement>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(predicate, trueChoice);

        TTargetQuery IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Coalesce<TTargetQuery>(params Func<IVertexGremlinQuery<TElement>, TTargetQuery>[] traversals) => Coalesce<TTargetQuery, TTargetQuery>(traversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Coalesce(params Func<IVertexGremlinQuery<TElement>, IGremlinQueryBase>[] traversals) => Coalesce<IGremlinQueryBase, IValueGremlinQuery<object>>(traversals);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Coin(double probability) => Coin(probability);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.CyclicPath() => CyclicPath();

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Dedup() => DedupGlobal();

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.DedupLocal() => DedupLocal();

        TTargetQuery IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.FlatMap<TTargetQuery>(Func<IVertexGremlinQuery<TElement>, TTargetQuery> mapping) => FlatMap(mapping);

        IArrayGremlinQuery<TElement[], TElement, IVertexGremlinQuery<TElement>> IGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.ForceArray() => CloneAs<IArrayGremlinQuery<TElement[], TElement, IVertexGremlinQuery<TElement>>>();

        IArrayGremlinQuery<TElement[], TElement, IVertexGremlinQuery<TElement>> IGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.Fold() => Fold<IVertexGremlinQuery<TElement>>();

        IValueGremlinQuery<IDictionary<TNewKey, TNewValue>> IGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.Group<TNewKey, TNewValue>(Func<IGroupBuilder<TElement, IVertexGremlinQuery<TElement>>, IGroupBuilderWithKeyAndValue<TNewKey, TNewValue>> groupBuilder) => Group(groupBuilder);
        IValueGremlinQuery<IDictionary<TNewKey, TElement[]>> IGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.Group<TNewKey>(Func<IGroupBuilder<TElement, IVertexGremlinQuery<TElement>>, IGroupBuilderWithKey<TElement, IVertexGremlinQuery<TElement>, TNewKey>> groupBuilder) => Group(groupBuilder);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Identity() => Identity();

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.Inject(params TElement[] elements) => Inject(elements);
        
        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Limit(long count) => LimitGlobal(count);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.LimitLocal(long count) => LimitLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Local<TTargetQuery>(Func<IVertexGremlinQuery<TElement> , TTargetQuery> localTraversal) => Local(localTraversal);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Loop(Func<IStartLoopBuilder<IVertexGremlinQuery<TElement>>, IFinalLoopBuilder<IVertexGremlinQuery<TElement>>> loopBuilderTransformation) => Loop(loopBuilderTransformation);

        TTargetQuery IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Map<TTargetQuery>(Func<IVertexGremlinQuery<TElement>, TTargetQuery> mapping) => Map(mapping);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Mute() => Mute();

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Not(Func<IVertexGremlinQuery<TElement>, IGremlinQueryBase> notTraversal) => Not(static (__, notTraversal) => notTraversal(__), notTraversal);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.None() => None();

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Optional(Func<IVertexGremlinQuery<TElement>, IVertexGremlinQuery<TElement>> optionalTraversal) => Optional(optionalTraversal);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Or(params Func<IVertexGremlinQuery<TElement>, IGremlinQueryBase>[] orTraversals) => Or(orTraversals);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.Order(Func<IOrderBuilder<TElement, IVertexGremlinQuery<TElement>>, IOrderBuilderWithBy<TElement, IVertexGremlinQuery<TElement>>> projection) => OrderGlobal(projection);
        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.OrderLocal(Func<IOrderBuilder<TElement, IVertexGremlinQuery<TElement>>, IOrderBuilderWithBy<TElement, IVertexGremlinQuery<TElement>>> projection) => OrderLocal(projection);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Order(Func<IOrderBuilder<IVertexGremlinQuery<TElement>>, IOrderBuilderWithBy<IVertexGremlinQuery<TElement>>> projection) => OrderGlobal(projection);
        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.OrderLocal(Func<IOrderBuilder<IVertexGremlinQuery<TElement>>, IOrderBuilderWithBy<IVertexGremlinQuery<TElement>>> projection) => OrderLocal(projection);

        IValueGremlinQuery<dynamic> IGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.Project(Func<IProjectBuilder<IVertexGremlinQuery<TElement>, TElement>, IProjectDynamicResult> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.Project<TResult>(Func<IProjectBuilder<IVertexGremlinQuery<TElement>, TElement>, IProjectMapResult<TResult>> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.Project<TResult>(Func<IProjectBuilder<IVertexGremlinQuery<TElement>, TElement>, IProjectTupleResult<TResult>> continuation) => Project<TResult>(continuation);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Range(long low, long high) => RangeGlobal(low, high);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.RangeLocal(long low, long high) => RangeLocal(low, high);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.SideEffect(Func<IVertexGremlinQuery<TElement>, IGremlinQueryBase> sideEffectTraversal) => SideEffect(sideEffectTraversal);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.SimplePath() => SimplePath();

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Skip(long count) => Skip(count, Scope.Global);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.SkipLocal(long count) => Skip(count, Scope.Local);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Tail(long count) => TailGlobal(count);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.TailLocal(long count) => TailLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Union<TTargetQuery>(params Func<IVertexGremlinQuery<TElement>, TTargetQuery>[] unionTraversals) => Union<TTargetQuery, TTargetQuery>(unionTraversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Union(params Func<IVertexGremlinQuery<TElement>, IGremlinQueryBase>[] unionTraversals) => Union<IGremlinQueryBase, IValueGremlinQuery<object>>(unionTraversals);

        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);
        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Where(ILambda lambda) => Where(lambda);
        IVertexGremlinQuery<TElement> IGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Where(Func<IVertexGremlinQuery<TElement>, IGremlinQueryBase> filterTraversal) => Where(filterTraversal);

        TTargetQuery IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.Aggregate<TTargetQuery>(Func<IEdgeGremlinQuery<TElement>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IEdgeGremlinQuery<TElement>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Global, continuation);
        TTargetQuery IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.AggregateLocal<TTargetQuery>(Func<IEdgeGremlinQuery<TElement>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IEdgeGremlinQuery<TElement>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Local, continuation);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.Aggregate(StepLabel<IArrayGremlinQuery<TElement[], TElement, IEdgeGremlinQuery<TElement>>, TElement[]> stepLabel) => Aggregate(Scope.Global, stepLabel);
        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.AggregateLocal(StepLabel<IArrayGremlinQuery<TElement[], TElement, IEdgeGremlinQuery<TElement>>, TElement[]> stepLabel) => Aggregate(Scope.Local, stepLabel);

        TTargetQuery IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.As<TTargetQuery>(Func<IEdgeGremlinQuery<TElement>, StepLabel<IEdgeGremlinQuery<TElement>, TElement>, TTargetQuery> continuation) => As<StepLabel<IEdgeGremlinQuery<TElement>, TElement>, TTargetQuery>(continuation);
        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.As(StepLabel<TElement> stepLabel) => As(stepLabel);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.And(params Func<IEdgeGremlinQuery<TElement>, IGremlinQueryBase>[] andTraversals) => And(andTraversals);

        TTargetQuery IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.As<TTargetQuery>(Func<IEdgeGremlinQuery<TElement>, StepLabel<IEdgeGremlinQuery<TElement>, object>, TTargetQuery> continuation) => As<StepLabel<IEdgeGremlinQuery<TElement>, object>, TTargetQuery>(continuation);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Barrier() => Barrier();

        TTargetQuery IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Choose<TTargetQuery>(Func<IEdgeGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IEdgeGremlinQuery<TElement>, TTargetQuery> trueChoice, Func<IEdgeGremlinQuery<TElement>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(traversalPredicate, trueChoice, falseChoice);
        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Choose(Func<IEdgeGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IEdgeGremlinQuery<TElement>, IEdgeGremlinQuery<TElement>> trueChoice) => Choose<IEdgeGremlinQuery<TElement>, IEdgeGremlinQuery<TElement>, IEdgeGremlinQuery<TElement>>(traversalPredicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Choose(Func<IEdgeGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IEdgeGremlinQuery<TElement>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(traversalPredicate, trueChoice);
        
        TTargetQuery IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Choose<TTargetQuery>(Func<IChooseBuilder<IEdgeGremlinQuery<TElement>>, IChooseBuilderWithCaseOrDefault<TTargetQuery>> continuation) => Choose<TTargetQuery>(continuation);

        TTargetQuery IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.Choose<TTargetQuery>(Expression<Func<TElement, bool>> predicate, Func<IEdgeGremlinQuery<TElement>, TTargetQuery> trueChoice, Func<IEdgeGremlinQuery<TElement>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(predicate, trueChoice, falseChoice);
        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IEdgeGremlinQuery<TElement>, IEdgeGremlinQuery<TElement>> trueChoice) => Choose<IEdgeGremlinQuery<TElement>, IEdgeGremlinQuery<TElement>, IEdgeGremlinQuery<TElement>>(predicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IEdgeGremlinQuery<TElement>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(predicate, trueChoice);

        TTargetQuery IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Coalesce<TTargetQuery>(params Func<IEdgeGremlinQuery<TElement>, TTargetQuery>[] traversals) => Coalesce<TTargetQuery, TTargetQuery>(traversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Coalesce(params Func<IEdgeGremlinQuery<TElement>, IGremlinQueryBase>[] traversals) => Coalesce<IGremlinQueryBase, IValueGremlinQuery<object>>(traversals);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Coin(double probability) => Coin(probability);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.CyclicPath() => CyclicPath();

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Dedup() => DedupGlobal();

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.DedupLocal() => DedupLocal();

        TTargetQuery IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.FlatMap<TTargetQuery>(Func<IEdgeGremlinQuery<TElement>, TTargetQuery> mapping) => FlatMap(mapping);

        IArrayGremlinQuery<TElement[], TElement, IEdgeGremlinQuery<TElement>> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.ForceArray() => CloneAs<IArrayGremlinQuery<TElement[], TElement, IEdgeGremlinQuery<TElement>>>();

        IArrayGremlinQuery<TElement[], TElement, IEdgeGremlinQuery<TElement>> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.Fold() => Fold<IEdgeGremlinQuery<TElement>>();

        IValueGremlinQuery<IDictionary<TNewKey, TNewValue>> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.Group<TNewKey, TNewValue>(Func<IGroupBuilder<TElement, IEdgeGremlinQuery<TElement>>, IGroupBuilderWithKeyAndValue<TNewKey, TNewValue>> groupBuilder) => Group(groupBuilder);
        IValueGremlinQuery<IDictionary<TNewKey, TElement[]>> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.Group<TNewKey>(Func<IGroupBuilder<TElement, IEdgeGremlinQuery<TElement>>, IGroupBuilderWithKey<TElement, IEdgeGremlinQuery<TElement>, TNewKey>> groupBuilder) => Group(groupBuilder);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Identity() => Identity();

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.Inject(params TElement[] elements) => Inject(elements);
        
        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Limit(long count) => LimitGlobal(count);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.LimitLocal(long count) => LimitLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Local<TTargetQuery>(Func<IEdgeGremlinQuery<TElement> , TTargetQuery> localTraversal) => Local(localTraversal);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Loop(Func<IStartLoopBuilder<IEdgeGremlinQuery<TElement>>, IFinalLoopBuilder<IEdgeGremlinQuery<TElement>>> loopBuilderTransformation) => Loop(loopBuilderTransformation);

        TTargetQuery IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Map<TTargetQuery>(Func<IEdgeGremlinQuery<TElement>, TTargetQuery> mapping) => Map(mapping);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Mute() => Mute();

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Not(Func<IEdgeGremlinQuery<TElement>, IGremlinQueryBase> notTraversal) => Not(static (__, notTraversal) => notTraversal(__), notTraversal);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.None() => None();

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Optional(Func<IEdgeGremlinQuery<TElement>, IEdgeGremlinQuery<TElement>> optionalTraversal) => Optional(optionalTraversal);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Or(params Func<IEdgeGremlinQuery<TElement>, IGremlinQueryBase>[] orTraversals) => Or(orTraversals);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.Order(Func<IOrderBuilder<TElement, IEdgeGremlinQuery<TElement>>, IOrderBuilderWithBy<TElement, IEdgeGremlinQuery<TElement>>> projection) => OrderGlobal(projection);
        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.OrderLocal(Func<IOrderBuilder<TElement, IEdgeGremlinQuery<TElement>>, IOrderBuilderWithBy<TElement, IEdgeGremlinQuery<TElement>>> projection) => OrderLocal(projection);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Order(Func<IOrderBuilder<IEdgeGremlinQuery<TElement>>, IOrderBuilderWithBy<IEdgeGremlinQuery<TElement>>> projection) => OrderGlobal(projection);
        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.OrderLocal(Func<IOrderBuilder<IEdgeGremlinQuery<TElement>>, IOrderBuilderWithBy<IEdgeGremlinQuery<TElement>>> projection) => OrderLocal(projection);

        IValueGremlinQuery<dynamic> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.Project(Func<IProjectBuilder<IEdgeGremlinQuery<TElement>, TElement>, IProjectDynamicResult> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.Project<TResult>(Func<IProjectBuilder<IEdgeGremlinQuery<TElement>, TElement>, IProjectMapResult<TResult>> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.Project<TResult>(Func<IProjectBuilder<IEdgeGremlinQuery<TElement>, TElement>, IProjectTupleResult<TResult>> continuation) => Project<TResult>(continuation);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Range(long low, long high) => RangeGlobal(low, high);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.RangeLocal(long low, long high) => RangeLocal(low, high);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.SideEffect(Func<IEdgeGremlinQuery<TElement>, IGremlinQueryBase> sideEffectTraversal) => SideEffect(sideEffectTraversal);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.SimplePath() => SimplePath();

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Skip(long count) => Skip(count, Scope.Global);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.SkipLocal(long count) => Skip(count, Scope.Local);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Tail(long count) => TailGlobal(count);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.TailLocal(long count) => TailLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Union<TTargetQuery>(params Func<IEdgeGremlinQuery<TElement>, TTargetQuery>[] unionTraversals) => Union<TTargetQuery, TTargetQuery>(unionTraversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Union(params Func<IEdgeGremlinQuery<TElement>, IGremlinQueryBase>[] unionTraversals) => Union<IGremlinQueryBase, IValueGremlinQuery<object>>(unionTraversals);

        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);
        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Where(ILambda lambda) => Where(lambda);
        IEdgeGremlinQuery<TElement> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Where(Func<IEdgeGremlinQuery<TElement>, IGremlinQueryBase> filterTraversal) => Where(filterTraversal);

        TTargetQuery IGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Aggregate<TTargetQuery>(Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Global, continuation);
        TTargetQuery IGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.AggregateLocal<TTargetQuery>(Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Local, continuation);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Aggregate(StepLabel<IArrayGremlinQuery<TElement[], TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>, TElement[]> stepLabel) => Aggregate(Scope.Global, stepLabel);
        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.AggregateLocal(StepLabel<IArrayGremlinQuery<TElement[], TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>, TElement[]> stepLabel) => Aggregate(Scope.Local, stepLabel);

        TTargetQuery IGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.As<TTargetQuery>(Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, StepLabel<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, TElement>, TTargetQuery> continuation) => As<StepLabel<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, TElement>, TTargetQuery>(continuation);
        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.As(StepLabel<TElement> stepLabel) => As(stepLabel);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.And(params Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase>[] andTraversals) => And(andTraversals);

        TTargetQuery IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.As<TTargetQuery>(Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, StepLabel<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, object>, TTargetQuery> continuation) => As<StepLabel<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, object>, TTargetQuery>(continuation);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Barrier() => Barrier();

        TTargetQuery IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Choose<TTargetQuery>(Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase> traversalPredicate, Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, TTargetQuery> trueChoice, Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(traversalPredicate, trueChoice, falseChoice);
        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Choose(Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase> traversalPredicate, Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>> trueChoice) => Choose<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>(traversalPredicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Choose(Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase> traversalPredicate, Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(traversalPredicate, trueChoice);
        
        TTargetQuery IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Choose<TTargetQuery>(Func<IChooseBuilder<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>, IChooseBuilderWithCaseOrDefault<TTargetQuery>> continuation) => Choose<TTargetQuery>(continuation);

        TTargetQuery IGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Choose<TTargetQuery>(Expression<Func<TElement, bool>> predicate, Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, TTargetQuery> trueChoice, Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(predicate, trueChoice, falseChoice);
        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>> trueChoice) => Choose<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>(predicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(predicate, trueChoice);

        TTargetQuery IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Coalesce<TTargetQuery>(params Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, TTargetQuery>[] traversals) => Coalesce<TTargetQuery, TTargetQuery>(traversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Coalesce(params Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase>[] traversals) => Coalesce<IGremlinQueryBase, IValueGremlinQuery<object>>(traversals);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Coin(double probability) => Coin(probability);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.CyclicPath() => CyclicPath();

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Dedup() => DedupGlobal();

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.DedupLocal() => DedupLocal();

        TTargetQuery IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.FlatMap<TTargetQuery>(Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, TTargetQuery> mapping) => FlatMap(mapping);

        IArrayGremlinQuery<TElement[], TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>> IGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.ForceArray() => CloneAs<IArrayGremlinQuery<TElement[], TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>>();

        IArrayGremlinQuery<TElement[], TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>> IGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Fold() => Fold<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>();

        IValueGremlinQuery<IDictionary<TNewKey, TNewValue>> IGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Group<TNewKey, TNewValue>(Func<IGroupBuilder<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>, IGroupBuilderWithKeyAndValue<TNewKey, TNewValue>> groupBuilder) => Group(groupBuilder);
        IValueGremlinQuery<IDictionary<TNewKey, TElement[]>> IGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Group<TNewKey>(Func<IGroupBuilder<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>, IGroupBuilderWithKey<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, TNewKey>> groupBuilder) => Group(groupBuilder);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Identity() => Identity();

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Inject(params TElement[] elements) => Inject(elements);
        
        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Limit(long count) => LimitGlobal(count);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.LimitLocal(long count) => LimitLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Local<TTargetQuery>(Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex> , TTargetQuery> localTraversal) => Local(localTraversal);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Loop(Func<IStartLoopBuilder<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>, IFinalLoopBuilder<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>> loopBuilderTransformation) => Loop(loopBuilderTransformation);

        TTargetQuery IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Map<TTargetQuery>(Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, TTargetQuery> mapping) => Map(mapping);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Mute() => Mute();

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Not(Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase> notTraversal) => Not(static (__, notTraversal) => notTraversal(__), notTraversal);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.None() => None();

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Optional(Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>> optionalTraversal) => Optional(optionalTraversal);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Or(params Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase>[] orTraversals) => Or(orTraversals);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Order(Func<IOrderBuilder<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>, IOrderBuilderWithBy<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>> projection) => OrderGlobal(projection);
        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.OrderLocal(Func<IOrderBuilder<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>, IOrderBuilderWithBy<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>> projection) => OrderLocal(projection);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Order(Func<IOrderBuilder<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>, IOrderBuilderWithBy<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>> projection) => OrderGlobal(projection);
        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.OrderLocal(Func<IOrderBuilder<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>, IOrderBuilderWithBy<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>> projection) => OrderLocal(projection);

        IValueGremlinQuery<dynamic> IGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Project(Func<IProjectBuilder<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, TElement>, IProjectDynamicResult> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Project<TResult>(Func<IProjectBuilder<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, TElement>, IProjectMapResult<TResult>> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Project<TResult>(Func<IProjectBuilder<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, TElement>, IProjectTupleResult<TResult>> continuation) => Project<TResult>(continuation);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Range(long low, long high) => RangeGlobal(low, high);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.RangeLocal(long low, long high) => RangeLocal(low, high);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.SideEffect(Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase> sideEffectTraversal) => SideEffect(sideEffectTraversal);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.SimplePath() => SimplePath();

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Skip(long count) => Skip(count, Scope.Global);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.SkipLocal(long count) => Skip(count, Scope.Local);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Tail(long count) => TailGlobal(count);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.TailLocal(long count) => TailLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Union<TTargetQuery>(params Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, TTargetQuery>[] unionTraversals) => Union<TTargetQuery, TTargetQuery>(unionTraversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Union(params Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase>[] unionTraversals) => Union<IGremlinQueryBase, IValueGremlinQuery<object>>(unionTraversals);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);
        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Where(ILambda lambda) => Where(lambda);
        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Where(Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase> filterTraversal) => Where(filterTraversal);

        TTargetQuery IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Aggregate<TTargetQuery>(Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Global, continuation);
        TTargetQuery IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.AggregateLocal<TTargetQuery>(Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Local, continuation);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Aggregate(StepLabel<IArrayGremlinQuery<TElement[], TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>, TElement[]> stepLabel) => Aggregate(Scope.Global, stepLabel);
        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.AggregateLocal(StepLabel<IArrayGremlinQuery<TElement[], TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>, TElement[]> stepLabel) => Aggregate(Scope.Local, stepLabel);

        TTargetQuery IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.As<TTargetQuery>(Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, StepLabel<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, TElement>, TTargetQuery> continuation) => As<StepLabel<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, TElement>, TTargetQuery>(continuation);
        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.As(StepLabel<TElement> stepLabel) => As(stepLabel);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.And(params Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IGremlinQueryBase>[] andTraversals) => And(andTraversals);

        TTargetQuery IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.As<TTargetQuery>(Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, StepLabel<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, object>, TTargetQuery> continuation) => As<StepLabel<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, object>, TTargetQuery>(continuation);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Barrier() => Barrier();

        TTargetQuery IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Choose<TTargetQuery>(Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IGremlinQueryBase> traversalPredicate, Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, TTargetQuery> trueChoice, Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(traversalPredicate, trueChoice, falseChoice);
        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Choose(Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IGremlinQueryBase> traversalPredicate, Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>> trueChoice) => Choose<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>(traversalPredicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Choose(Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IGremlinQueryBase> traversalPredicate, Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(traversalPredicate, trueChoice);
        
        TTargetQuery IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Choose<TTargetQuery>(Func<IChooseBuilder<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>, IChooseBuilderWithCaseOrDefault<TTargetQuery>> continuation) => Choose<TTargetQuery>(continuation);

        TTargetQuery IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Choose<TTargetQuery>(Expression<Func<TElement, bool>> predicate, Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, TTargetQuery> trueChoice, Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(predicate, trueChoice, falseChoice);
        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>> trueChoice) => Choose<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>(predicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(predicate, trueChoice);

        TTargetQuery IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Coalesce<TTargetQuery>(params Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, TTargetQuery>[] traversals) => Coalesce<TTargetQuery, TTargetQuery>(traversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Coalesce(params Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IGremlinQueryBase>[] traversals) => Coalesce<IGremlinQueryBase, IValueGremlinQuery<object>>(traversals);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Coin(double probability) => Coin(probability);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.CyclicPath() => CyclicPath();

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Dedup() => DedupGlobal();

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.DedupLocal() => DedupLocal();

        TTargetQuery IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.FlatMap<TTargetQuery>(Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, TTargetQuery> mapping) => FlatMap(mapping);

        IArrayGremlinQuery<TElement[], TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.ForceArray() => CloneAs<IArrayGremlinQuery<TElement[], TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>>();

        IArrayGremlinQuery<TElement[], TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Fold() => Fold<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>();

        IValueGremlinQuery<IDictionary<TNewKey, TNewValue>> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Group<TNewKey, TNewValue>(Func<IGroupBuilder<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>, IGroupBuilderWithKeyAndValue<TNewKey, TNewValue>> groupBuilder) => Group(groupBuilder);
        IValueGremlinQuery<IDictionary<TNewKey, TElement[]>> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Group<TNewKey>(Func<IGroupBuilder<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>, IGroupBuilderWithKey<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, TNewKey>> groupBuilder) => Group(groupBuilder);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Identity() => Identity();

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Inject(params TElement[] elements) => Inject(elements);
        
        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Limit(long count) => LimitGlobal(count);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.LimitLocal(long count) => LimitLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Local<TTargetQuery>(Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> , TTargetQuery> localTraversal) => Local(localTraversal);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Loop(Func<IStartLoopBuilder<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>, IFinalLoopBuilder<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>> loopBuilderTransformation) => Loop(loopBuilderTransformation);

        TTargetQuery IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Map<TTargetQuery>(Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, TTargetQuery> mapping) => Map(mapping);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Mute() => Mute();

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Not(Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IGremlinQueryBase> notTraversal) => Not(static (__, notTraversal) => notTraversal(__), notTraversal);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.None() => None();

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Optional(Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>> optionalTraversal) => Optional(optionalTraversal);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Or(params Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IGremlinQueryBase>[] orTraversals) => Or(orTraversals);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Order(Func<IOrderBuilder<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>, IOrderBuilderWithBy<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>> projection) => OrderGlobal(projection);
        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.OrderLocal(Func<IOrderBuilder<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>, IOrderBuilderWithBy<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>> projection) => OrderLocal(projection);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Order(Func<IOrderBuilder<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>, IOrderBuilderWithBy<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>> projection) => OrderGlobal(projection);
        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.OrderLocal(Func<IOrderBuilder<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>, IOrderBuilderWithBy<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>> projection) => OrderLocal(projection);

        IValueGremlinQuery<dynamic> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Project(Func<IProjectBuilder<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, TElement>, IProjectDynamicResult> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Project<TResult>(Func<IProjectBuilder<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, TElement>, IProjectMapResult<TResult>> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Project<TResult>(Func<IProjectBuilder<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, TElement>, IProjectTupleResult<TResult>> continuation) => Project<TResult>(continuation);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Range(long low, long high) => RangeGlobal(low, high);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.RangeLocal(long low, long high) => RangeLocal(low, high);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.SideEffect(Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IGremlinQueryBase> sideEffectTraversal) => SideEffect(sideEffectTraversal);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.SimplePath() => SimplePath();

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Skip(long count) => Skip(count, Scope.Global);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.SkipLocal(long count) => Skip(count, Scope.Local);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Tail(long count) => TailGlobal(count);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.TailLocal(long count) => TailLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Union<TTargetQuery>(params Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, TTargetQuery>[] unionTraversals) => Union<TTargetQuery, TTargetQuery>(unionTraversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Union(params Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IGremlinQueryBase>[] unionTraversals) => Union<IGremlinQueryBase, IValueGremlinQuery<object>>(unionTraversals);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);
        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Where(ILambda lambda) => Where(lambda);
        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Where(Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IGremlinQueryBase> filterTraversal) => Where(filterTraversal);

        TTargetQuery IGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.Aggregate<TTargetQuery>(Func<IInEdgeGremlinQuery<TElement, TInVertex>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IInEdgeGremlinQuery<TElement, TInVertex>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Global, continuation);
        TTargetQuery IGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.AggregateLocal<TTargetQuery>(Func<IInEdgeGremlinQuery<TElement, TInVertex>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IInEdgeGremlinQuery<TElement, TInVertex>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Local, continuation);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.Aggregate(StepLabel<IArrayGremlinQuery<TElement[], TElement, IInEdgeGremlinQuery<TElement, TInVertex>>, TElement[]> stepLabel) => Aggregate(Scope.Global, stepLabel);
        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.AggregateLocal(StepLabel<IArrayGremlinQuery<TElement[], TElement, IInEdgeGremlinQuery<TElement, TInVertex>>, TElement[]> stepLabel) => Aggregate(Scope.Local, stepLabel);

        TTargetQuery IGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.As<TTargetQuery>(Func<IInEdgeGremlinQuery<TElement, TInVertex>, StepLabel<IInEdgeGremlinQuery<TElement, TInVertex>, TElement>, TTargetQuery> continuation) => As<StepLabel<IInEdgeGremlinQuery<TElement, TInVertex>, TElement>, TTargetQuery>(continuation);
        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.As(StepLabel<TElement> stepLabel) => As(stepLabel);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.And(params Func<IInEdgeGremlinQuery<TElement, TInVertex>, IGremlinQueryBase>[] andTraversals) => And(andTraversals);

        TTargetQuery IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.As<TTargetQuery>(Func<IInEdgeGremlinQuery<TElement, TInVertex>, StepLabel<IInEdgeGremlinQuery<TElement, TInVertex>, object>, TTargetQuery> continuation) => As<StepLabel<IInEdgeGremlinQuery<TElement, TInVertex>, object>, TTargetQuery>(continuation);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Barrier() => Barrier();

        TTargetQuery IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Choose<TTargetQuery>(Func<IInEdgeGremlinQuery<TElement, TInVertex>, IGremlinQueryBase> traversalPredicate, Func<IInEdgeGremlinQuery<TElement, TInVertex>, TTargetQuery> trueChoice, Func<IInEdgeGremlinQuery<TElement, TInVertex>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(traversalPredicate, trueChoice, falseChoice);
        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Choose(Func<IInEdgeGremlinQuery<TElement, TInVertex>, IGremlinQueryBase> traversalPredicate, Func<IInEdgeGremlinQuery<TElement, TInVertex>, IInEdgeGremlinQuery<TElement, TInVertex>> trueChoice) => Choose<IInEdgeGremlinQuery<TElement, TInVertex>, IInEdgeGremlinQuery<TElement, TInVertex>, IInEdgeGremlinQuery<TElement, TInVertex>>(traversalPredicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Choose(Func<IInEdgeGremlinQuery<TElement, TInVertex>, IGremlinQueryBase> traversalPredicate, Func<IInEdgeGremlinQuery<TElement, TInVertex>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(traversalPredicate, trueChoice);
        
        TTargetQuery IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Choose<TTargetQuery>(Func<IChooseBuilder<IInEdgeGremlinQuery<TElement, TInVertex>>, IChooseBuilderWithCaseOrDefault<TTargetQuery>> continuation) => Choose<TTargetQuery>(continuation);

        TTargetQuery IGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.Choose<TTargetQuery>(Expression<Func<TElement, bool>> predicate, Func<IInEdgeGremlinQuery<TElement, TInVertex>, TTargetQuery> trueChoice, Func<IInEdgeGremlinQuery<TElement, TInVertex>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(predicate, trueChoice, falseChoice);
        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IInEdgeGremlinQuery<TElement, TInVertex>, IInEdgeGremlinQuery<TElement, TInVertex>> trueChoice) => Choose<IInEdgeGremlinQuery<TElement, TInVertex>, IInEdgeGremlinQuery<TElement, TInVertex>, IInEdgeGremlinQuery<TElement, TInVertex>>(predicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IInEdgeGremlinQuery<TElement, TInVertex>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(predicate, trueChoice);

        TTargetQuery IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Coalesce<TTargetQuery>(params Func<IInEdgeGremlinQuery<TElement, TInVertex>, TTargetQuery>[] traversals) => Coalesce<TTargetQuery, TTargetQuery>(traversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Coalesce(params Func<IInEdgeGremlinQuery<TElement, TInVertex>, IGremlinQueryBase>[] traversals) => Coalesce<IGremlinQueryBase, IValueGremlinQuery<object>>(traversals);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Coin(double probability) => Coin(probability);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.CyclicPath() => CyclicPath();

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Dedup() => DedupGlobal();

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.DedupLocal() => DedupLocal();

        TTargetQuery IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.FlatMap<TTargetQuery>(Func<IInEdgeGremlinQuery<TElement, TInVertex>, TTargetQuery> mapping) => FlatMap(mapping);

        IArrayGremlinQuery<TElement[], TElement, IInEdgeGremlinQuery<TElement, TInVertex>> IGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.ForceArray() => CloneAs<IArrayGremlinQuery<TElement[], TElement, IInEdgeGremlinQuery<TElement, TInVertex>>>();

        IArrayGremlinQuery<TElement[], TElement, IInEdgeGremlinQuery<TElement, TInVertex>> IGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.Fold() => Fold<IInEdgeGremlinQuery<TElement, TInVertex>>();

        IValueGremlinQuery<IDictionary<TNewKey, TNewValue>> IGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.Group<TNewKey, TNewValue>(Func<IGroupBuilder<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>, IGroupBuilderWithKeyAndValue<TNewKey, TNewValue>> groupBuilder) => Group(groupBuilder);
        IValueGremlinQuery<IDictionary<TNewKey, TElement[]>> IGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.Group<TNewKey>(Func<IGroupBuilder<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>, IGroupBuilderWithKey<TElement, IInEdgeGremlinQuery<TElement, TInVertex>, TNewKey>> groupBuilder) => Group(groupBuilder);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Identity() => Identity();

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.Inject(params TElement[] elements) => Inject(elements);
        
        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Limit(long count) => LimitGlobal(count);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.LimitLocal(long count) => LimitLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Local<TTargetQuery>(Func<IInEdgeGremlinQuery<TElement, TInVertex> , TTargetQuery> localTraversal) => Local(localTraversal);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Loop(Func<IStartLoopBuilder<IInEdgeGremlinQuery<TElement, TInVertex>>, IFinalLoopBuilder<IInEdgeGremlinQuery<TElement, TInVertex>>> loopBuilderTransformation) => Loop(loopBuilderTransformation);

        TTargetQuery IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Map<TTargetQuery>(Func<IInEdgeGremlinQuery<TElement, TInVertex>, TTargetQuery> mapping) => Map(mapping);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Mute() => Mute();

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Not(Func<IInEdgeGremlinQuery<TElement, TInVertex>, IGremlinQueryBase> notTraversal) => Not(static (__, notTraversal) => notTraversal(__), notTraversal);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.None() => None();

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Optional(Func<IInEdgeGremlinQuery<TElement, TInVertex>, IInEdgeGremlinQuery<TElement, TInVertex>> optionalTraversal) => Optional(optionalTraversal);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Or(params Func<IInEdgeGremlinQuery<TElement, TInVertex>, IGremlinQueryBase>[] orTraversals) => Or(orTraversals);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.Order(Func<IOrderBuilder<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>, IOrderBuilderWithBy<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>> projection) => OrderGlobal(projection);
        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.OrderLocal(Func<IOrderBuilder<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>, IOrderBuilderWithBy<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>> projection) => OrderLocal(projection);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Order(Func<IOrderBuilder<IInEdgeGremlinQuery<TElement, TInVertex>>, IOrderBuilderWithBy<IInEdgeGremlinQuery<TElement, TInVertex>>> projection) => OrderGlobal(projection);
        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.OrderLocal(Func<IOrderBuilder<IInEdgeGremlinQuery<TElement, TInVertex>>, IOrderBuilderWithBy<IInEdgeGremlinQuery<TElement, TInVertex>>> projection) => OrderLocal(projection);

        IValueGremlinQuery<dynamic> IGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.Project(Func<IProjectBuilder<IInEdgeGremlinQuery<TElement, TInVertex>, TElement>, IProjectDynamicResult> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.Project<TResult>(Func<IProjectBuilder<IInEdgeGremlinQuery<TElement, TInVertex>, TElement>, IProjectMapResult<TResult>> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.Project<TResult>(Func<IProjectBuilder<IInEdgeGremlinQuery<TElement, TInVertex>, TElement>, IProjectTupleResult<TResult>> continuation) => Project<TResult>(continuation);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Range(long low, long high) => RangeGlobal(low, high);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.RangeLocal(long low, long high) => RangeLocal(low, high);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.SideEffect(Func<IInEdgeGremlinQuery<TElement, TInVertex>, IGremlinQueryBase> sideEffectTraversal) => SideEffect(sideEffectTraversal);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.SimplePath() => SimplePath();

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Skip(long count) => Skip(count, Scope.Global);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.SkipLocal(long count) => Skip(count, Scope.Local);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Tail(long count) => TailGlobal(count);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.TailLocal(long count) => TailLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Union<TTargetQuery>(params Func<IInEdgeGremlinQuery<TElement, TInVertex>, TTargetQuery>[] unionTraversals) => Union<TTargetQuery, TTargetQuery>(unionTraversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Union(params Func<IInEdgeGremlinQuery<TElement, TInVertex>, IGremlinQueryBase>[] unionTraversals) => Union<IGremlinQueryBase, IValueGremlinQuery<object>>(unionTraversals);

        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);
        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Where(ILambda lambda) => Where(lambda);
        IInEdgeGremlinQuery<TElement, TInVertex> IGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Where(Func<IInEdgeGremlinQuery<TElement, TInVertex>, IGremlinQueryBase> filterTraversal) => Where(filterTraversal);

        TTargetQuery IGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.Aggregate<TTargetQuery>(Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Global, continuation);
        TTargetQuery IGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.AggregateLocal<TTargetQuery>(Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Local, continuation);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.Aggregate(StepLabel<IArrayGremlinQuery<TElement[], TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>, TElement[]> stepLabel) => Aggregate(Scope.Global, stepLabel);
        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.AggregateLocal(StepLabel<IArrayGremlinQuery<TElement[], TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>, TElement[]> stepLabel) => Aggregate(Scope.Local, stepLabel);

        TTargetQuery IGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.As<TTargetQuery>(Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, StepLabel<IOutEdgeGremlinQuery<TElement, TOutVertex>, TElement>, TTargetQuery> continuation) => As<StepLabel<IOutEdgeGremlinQuery<TElement, TOutVertex>, TElement>, TTargetQuery>(continuation);
        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.As(StepLabel<TElement> stepLabel) => As(stepLabel);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.And(params Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase>[] andTraversals) => And(andTraversals);

        TTargetQuery IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.As<TTargetQuery>(Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, StepLabel<IOutEdgeGremlinQuery<TElement, TOutVertex>, object>, TTargetQuery> continuation) => As<StepLabel<IOutEdgeGremlinQuery<TElement, TOutVertex>, object>, TTargetQuery>(continuation);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Barrier() => Barrier();

        TTargetQuery IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Choose<TTargetQuery>(Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase> traversalPredicate, Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, TTargetQuery> trueChoice, Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(traversalPredicate, trueChoice, falseChoice);
        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Choose(Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase> traversalPredicate, Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, IOutEdgeGremlinQuery<TElement, TOutVertex>> trueChoice) => Choose<IOutEdgeGremlinQuery<TElement, TOutVertex>, IOutEdgeGremlinQuery<TElement, TOutVertex>, IOutEdgeGremlinQuery<TElement, TOutVertex>>(traversalPredicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Choose(Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase> traversalPredicate, Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(traversalPredicate, trueChoice);
        
        TTargetQuery IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Choose<TTargetQuery>(Func<IChooseBuilder<IOutEdgeGremlinQuery<TElement, TOutVertex>>, IChooseBuilderWithCaseOrDefault<TTargetQuery>> continuation) => Choose<TTargetQuery>(continuation);

        TTargetQuery IGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.Choose<TTargetQuery>(Expression<Func<TElement, bool>> predicate, Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, TTargetQuery> trueChoice, Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(predicate, trueChoice, falseChoice);
        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, IOutEdgeGremlinQuery<TElement, TOutVertex>> trueChoice) => Choose<IOutEdgeGremlinQuery<TElement, TOutVertex>, IOutEdgeGremlinQuery<TElement, TOutVertex>, IOutEdgeGremlinQuery<TElement, TOutVertex>>(predicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(predicate, trueChoice);

        TTargetQuery IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Coalesce<TTargetQuery>(params Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, TTargetQuery>[] traversals) => Coalesce<TTargetQuery, TTargetQuery>(traversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Coalesce(params Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase>[] traversals) => Coalesce<IGremlinQueryBase, IValueGremlinQuery<object>>(traversals);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Coin(double probability) => Coin(probability);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.CyclicPath() => CyclicPath();

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Dedup() => DedupGlobal();

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.DedupLocal() => DedupLocal();

        TTargetQuery IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.FlatMap<TTargetQuery>(Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, TTargetQuery> mapping) => FlatMap(mapping);

        IArrayGremlinQuery<TElement[], TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>> IGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.ForceArray() => CloneAs<IArrayGremlinQuery<TElement[], TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>>();

        IArrayGremlinQuery<TElement[], TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>> IGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.Fold() => Fold<IOutEdgeGremlinQuery<TElement, TOutVertex>>();

        IValueGremlinQuery<IDictionary<TNewKey, TNewValue>> IGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.Group<TNewKey, TNewValue>(Func<IGroupBuilder<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>, IGroupBuilderWithKeyAndValue<TNewKey, TNewValue>> groupBuilder) => Group(groupBuilder);
        IValueGremlinQuery<IDictionary<TNewKey, TElement[]>> IGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.Group<TNewKey>(Func<IGroupBuilder<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>, IGroupBuilderWithKey<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>, TNewKey>> groupBuilder) => Group(groupBuilder);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Identity() => Identity();

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.Inject(params TElement[] elements) => Inject(elements);
        
        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Limit(long count) => LimitGlobal(count);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.LimitLocal(long count) => LimitLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Local<TTargetQuery>(Func<IOutEdgeGremlinQuery<TElement, TOutVertex> , TTargetQuery> localTraversal) => Local(localTraversal);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Loop(Func<IStartLoopBuilder<IOutEdgeGremlinQuery<TElement, TOutVertex>>, IFinalLoopBuilder<IOutEdgeGremlinQuery<TElement, TOutVertex>>> loopBuilderTransformation) => Loop(loopBuilderTransformation);

        TTargetQuery IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Map<TTargetQuery>(Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, TTargetQuery> mapping) => Map(mapping);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Mute() => Mute();

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Not(Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase> notTraversal) => Not(static (__, notTraversal) => notTraversal(__), notTraversal);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.None() => None();

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Optional(Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, IOutEdgeGremlinQuery<TElement, TOutVertex>> optionalTraversal) => Optional(optionalTraversal);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Or(params Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase>[] orTraversals) => Or(orTraversals);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.Order(Func<IOrderBuilder<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>, IOrderBuilderWithBy<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>> projection) => OrderGlobal(projection);
        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.OrderLocal(Func<IOrderBuilder<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>, IOrderBuilderWithBy<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>> projection) => OrderLocal(projection);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Order(Func<IOrderBuilder<IOutEdgeGremlinQuery<TElement, TOutVertex>>, IOrderBuilderWithBy<IOutEdgeGremlinQuery<TElement, TOutVertex>>> projection) => OrderGlobal(projection);
        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.OrderLocal(Func<IOrderBuilder<IOutEdgeGremlinQuery<TElement, TOutVertex>>, IOrderBuilderWithBy<IOutEdgeGremlinQuery<TElement, TOutVertex>>> projection) => OrderLocal(projection);

        IValueGremlinQuery<dynamic> IGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.Project(Func<IProjectBuilder<IOutEdgeGremlinQuery<TElement, TOutVertex>, TElement>, IProjectDynamicResult> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.Project<TResult>(Func<IProjectBuilder<IOutEdgeGremlinQuery<TElement, TOutVertex>, TElement>, IProjectMapResult<TResult>> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.Project<TResult>(Func<IProjectBuilder<IOutEdgeGremlinQuery<TElement, TOutVertex>, TElement>, IProjectTupleResult<TResult>> continuation) => Project<TResult>(continuation);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Range(long low, long high) => RangeGlobal(low, high);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.RangeLocal(long low, long high) => RangeLocal(low, high);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.SideEffect(Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase> sideEffectTraversal) => SideEffect(sideEffectTraversal);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.SimplePath() => SimplePath();

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Skip(long count) => Skip(count, Scope.Global);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.SkipLocal(long count) => Skip(count, Scope.Local);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Tail(long count) => TailGlobal(count);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.TailLocal(long count) => TailLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Union<TTargetQuery>(params Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, TTargetQuery>[] unionTraversals) => Union<TTargetQuery, TTargetQuery>(unionTraversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Union(params Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase>[] unionTraversals) => Union<IGremlinQueryBase, IValueGremlinQuery<object>>(unionTraversals);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);
        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Where(ILambda lambda) => Where(lambda);
        IOutEdgeGremlinQuery<TElement, TOutVertex> IGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Where(Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase> filterTraversal) => Where(filterTraversal);

        TTargetQuery IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.Aggregate<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TElement, TScalar>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Global, continuation);
        TTargetQuery IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.AggregateLocal<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TElement, TScalar>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Local, continuation);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.Aggregate(StepLabel<IArrayGremlinQuery<TElement[], TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>, TElement[]> stepLabel) => Aggregate(Scope.Global, stepLabel);
        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.AggregateLocal(StepLabel<IArrayGremlinQuery<TElement[], TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>, TElement[]> stepLabel) => Aggregate(Scope.Local, stepLabel);

        TTargetQuery IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.As<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TElement, TScalar>, StepLabel<IVertexPropertyGremlinQuery<TElement, TScalar>, TElement>, TTargetQuery> continuation) => As<StepLabel<IVertexPropertyGremlinQuery<TElement, TScalar>, TElement>, TTargetQuery>(continuation);
        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.As(StepLabel<TElement> stepLabel) => As(stepLabel);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.And(params Func<IVertexPropertyGremlinQuery<TElement, TScalar>, IGremlinQueryBase>[] andTraversals) => And(andTraversals);

        TTargetQuery IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.As<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TElement, TScalar>, StepLabel<IVertexPropertyGremlinQuery<TElement, TScalar>, object>, TTargetQuery> continuation) => As<StepLabel<IVertexPropertyGremlinQuery<TElement, TScalar>, object>, TTargetQuery>(continuation);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Barrier() => Barrier();

        TTargetQuery IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Choose<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TElement, TScalar>, IGremlinQueryBase> traversalPredicate, Func<IVertexPropertyGremlinQuery<TElement, TScalar>, TTargetQuery> trueChoice, Func<IVertexPropertyGremlinQuery<TElement, TScalar>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(traversalPredicate, trueChoice, falseChoice);
        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Choose(Func<IVertexPropertyGremlinQuery<TElement, TScalar>, IGremlinQueryBase> traversalPredicate, Func<IVertexPropertyGremlinQuery<TElement, TScalar>, IVertexPropertyGremlinQuery<TElement, TScalar>> trueChoice) => Choose<IVertexPropertyGremlinQuery<TElement, TScalar>, IVertexPropertyGremlinQuery<TElement, TScalar>, IVertexPropertyGremlinQuery<TElement, TScalar>>(traversalPredicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Choose(Func<IVertexPropertyGremlinQuery<TElement, TScalar>, IGremlinQueryBase> traversalPredicate, Func<IVertexPropertyGremlinQuery<TElement, TScalar>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(traversalPredicate, trueChoice);
        
        TTargetQuery IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Choose<TTargetQuery>(Func<IChooseBuilder<IVertexPropertyGremlinQuery<TElement, TScalar>>, IChooseBuilderWithCaseOrDefault<TTargetQuery>> continuation) => Choose<TTargetQuery>(continuation);

        TTargetQuery IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.Choose<TTargetQuery>(Expression<Func<TElement, bool>> predicate, Func<IVertexPropertyGremlinQuery<TElement, TScalar>, TTargetQuery> trueChoice, Func<IVertexPropertyGremlinQuery<TElement, TScalar>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(predicate, trueChoice, falseChoice);
        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IVertexPropertyGremlinQuery<TElement, TScalar>, IVertexPropertyGremlinQuery<TElement, TScalar>> trueChoice) => Choose<IVertexPropertyGremlinQuery<TElement, TScalar>, IVertexPropertyGremlinQuery<TElement, TScalar>, IVertexPropertyGremlinQuery<TElement, TScalar>>(predicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IVertexPropertyGremlinQuery<TElement, TScalar>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(predicate, trueChoice);

        TTargetQuery IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Coalesce<TTargetQuery>(params Func<IVertexPropertyGremlinQuery<TElement, TScalar>, TTargetQuery>[] traversals) => Coalesce<TTargetQuery, TTargetQuery>(traversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Coalesce(params Func<IVertexPropertyGremlinQuery<TElement, TScalar>, IGremlinQueryBase>[] traversals) => Coalesce<IGremlinQueryBase, IValueGremlinQuery<object>>(traversals);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Coin(double probability) => Coin(probability);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.CyclicPath() => CyclicPath();

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Dedup() => DedupGlobal();

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.DedupLocal() => DedupLocal();

        TTargetQuery IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.FlatMap<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TElement, TScalar>, TTargetQuery> mapping) => FlatMap(mapping);

        IArrayGremlinQuery<TElement[], TElement, IVertexPropertyGremlinQuery<TElement, TScalar>> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.ForceArray() => CloneAs<IArrayGremlinQuery<TElement[], TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>>();

        IArrayGremlinQuery<TElement[], TElement, IVertexPropertyGremlinQuery<TElement, TScalar>> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.Fold() => Fold<IVertexPropertyGremlinQuery<TElement, TScalar>>();

        IValueGremlinQuery<IDictionary<TNewKey, TNewValue>> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.Group<TNewKey, TNewValue>(Func<IGroupBuilder<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>, IGroupBuilderWithKeyAndValue<TNewKey, TNewValue>> groupBuilder) => Group(groupBuilder);
        IValueGremlinQuery<IDictionary<TNewKey, TElement[]>> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.Group<TNewKey>(Func<IGroupBuilder<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>, IGroupBuilderWithKey<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>, TNewKey>> groupBuilder) => Group(groupBuilder);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Identity() => Identity();

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.Inject(params TElement[] elements) => Inject(elements);
        
        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Limit(long count) => LimitGlobal(count);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.LimitLocal(long count) => LimitLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Local<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TElement, TScalar> , TTargetQuery> localTraversal) => Local(localTraversal);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Loop(Func<IStartLoopBuilder<IVertexPropertyGremlinQuery<TElement, TScalar>>, IFinalLoopBuilder<IVertexPropertyGremlinQuery<TElement, TScalar>>> loopBuilderTransformation) => Loop(loopBuilderTransformation);

        TTargetQuery IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Map<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TElement, TScalar>, TTargetQuery> mapping) => Map(mapping);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Mute() => Mute();

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Not(Func<IVertexPropertyGremlinQuery<TElement, TScalar>, IGremlinQueryBase> notTraversal) => Not(static (__, notTraversal) => notTraversal(__), notTraversal);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.None() => None();

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Optional(Func<IVertexPropertyGremlinQuery<TElement, TScalar>, IVertexPropertyGremlinQuery<TElement, TScalar>> optionalTraversal) => Optional(optionalTraversal);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Or(params Func<IVertexPropertyGremlinQuery<TElement, TScalar>, IGremlinQueryBase>[] orTraversals) => Or(orTraversals);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.Order(Func<IOrderBuilder<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>, IOrderBuilderWithBy<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>> projection) => OrderGlobal(projection);
        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.OrderLocal(Func<IOrderBuilder<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>, IOrderBuilderWithBy<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>> projection) => OrderLocal(projection);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Order(Func<IOrderBuilder<IVertexPropertyGremlinQuery<TElement, TScalar>>, IOrderBuilderWithBy<IVertexPropertyGremlinQuery<TElement, TScalar>>> projection) => OrderGlobal(projection);
        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.OrderLocal(Func<IOrderBuilder<IVertexPropertyGremlinQuery<TElement, TScalar>>, IOrderBuilderWithBy<IVertexPropertyGremlinQuery<TElement, TScalar>>> projection) => OrderLocal(projection);

        IValueGremlinQuery<dynamic> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.Project(Func<IProjectBuilder<IVertexPropertyGremlinQuery<TElement, TScalar>, TElement>, IProjectDynamicResult> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.Project<TResult>(Func<IProjectBuilder<IVertexPropertyGremlinQuery<TElement, TScalar>, TElement>, IProjectMapResult<TResult>> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.Project<TResult>(Func<IProjectBuilder<IVertexPropertyGremlinQuery<TElement, TScalar>, TElement>, IProjectTupleResult<TResult>> continuation) => Project<TResult>(continuation);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Range(long low, long high) => RangeGlobal(low, high);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.RangeLocal(long low, long high) => RangeLocal(low, high);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.SideEffect(Func<IVertexPropertyGremlinQuery<TElement, TScalar>, IGremlinQueryBase> sideEffectTraversal) => SideEffect(sideEffectTraversal);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.SimplePath() => SimplePath();

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Skip(long count) => Skip(count, Scope.Global);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.SkipLocal(long count) => Skip(count, Scope.Local);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Tail(long count) => TailGlobal(count);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.TailLocal(long count) => TailLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Union<TTargetQuery>(params Func<IVertexPropertyGremlinQuery<TElement, TScalar>, TTargetQuery>[] unionTraversals) => Union<TTargetQuery, TTargetQuery>(unionTraversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Union(params Func<IVertexPropertyGremlinQuery<TElement, TScalar>, IGremlinQueryBase>[] unionTraversals) => Union<IGremlinQueryBase, IValueGremlinQuery<object>>(unionTraversals);

        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);
        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Where(ILambda lambda) => Where(lambda);
        IVertexPropertyGremlinQuery<TElement, TScalar> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Where(Func<IVertexPropertyGremlinQuery<TElement, TScalar>, IGremlinQueryBase> filterTraversal) => Where(filterTraversal);

        TTargetQuery IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Aggregate<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Global, continuation);
        TTargetQuery IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.AggregateLocal<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Local, continuation);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Aggregate(StepLabel<IArrayGremlinQuery<TElement[], TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>, TElement[]> stepLabel) => Aggregate(Scope.Global, stepLabel);
        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.AggregateLocal(StepLabel<IArrayGremlinQuery<TElement[], TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>, TElement[]> stepLabel) => Aggregate(Scope.Local, stepLabel);

        TTargetQuery IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.As<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, StepLabel<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, TElement>, TTargetQuery> continuation) => As<StepLabel<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, TElement>, TTargetQuery>(continuation);
        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.As(StepLabel<TElement> stepLabel) => As(stepLabel);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.And(params Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IGremlinQueryBase>[] andTraversals) => And(andTraversals);

        TTargetQuery IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.As<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, StepLabel<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, object>, TTargetQuery> continuation) => As<StepLabel<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, object>, TTargetQuery>(continuation);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Barrier() => Barrier();

        TTargetQuery IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Choose<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IGremlinQueryBase> traversalPredicate, Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, TTargetQuery> trueChoice, Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(traversalPredicate, trueChoice, falseChoice);
        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Choose(Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IGremlinQueryBase> traversalPredicate, Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>> trueChoice) => Choose<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>(traversalPredicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Choose(Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IGremlinQueryBase> traversalPredicate, Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(traversalPredicate, trueChoice);
        
        TTargetQuery IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Choose<TTargetQuery>(Func<IChooseBuilder<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>, IChooseBuilderWithCaseOrDefault<TTargetQuery>> continuation) => Choose<TTargetQuery>(continuation);

        TTargetQuery IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Choose<TTargetQuery>(Expression<Func<TElement, bool>> predicate, Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, TTargetQuery> trueChoice, Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(predicate, trueChoice, falseChoice);
        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>> trueChoice) => Choose<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>(predicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(predicate, trueChoice);

        TTargetQuery IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Coalesce<TTargetQuery>(params Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, TTargetQuery>[] traversals) => Coalesce<TTargetQuery, TTargetQuery>(traversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Coalesce(params Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IGremlinQueryBase>[] traversals) => Coalesce<IGremlinQueryBase, IValueGremlinQuery<object>>(traversals);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Coin(double probability) => Coin(probability);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.CyclicPath() => CyclicPath();

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Dedup() => DedupGlobal();

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.DedupLocal() => DedupLocal();

        TTargetQuery IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.FlatMap<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, TTargetQuery> mapping) => FlatMap(mapping);

        IArrayGremlinQuery<TElement[], TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.ForceArray() => CloneAs<IArrayGremlinQuery<TElement[], TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>>();

        IArrayGremlinQuery<TElement[], TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Fold() => Fold<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>();

        IValueGremlinQuery<IDictionary<TNewKey, TNewValue>> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Group<TNewKey, TNewValue>(Func<IGroupBuilder<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>, IGroupBuilderWithKeyAndValue<TNewKey, TNewValue>> groupBuilder) => Group(groupBuilder);
        IValueGremlinQuery<IDictionary<TNewKey, TElement[]>> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Group<TNewKey>(Func<IGroupBuilder<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>, IGroupBuilderWithKey<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, TNewKey>> groupBuilder) => Group(groupBuilder);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Identity() => Identity();

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Inject(params TElement[] elements) => Inject(elements);
        
        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Limit(long count) => LimitGlobal(count);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.LimitLocal(long count) => LimitLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Local<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> , TTargetQuery> localTraversal) => Local(localTraversal);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Loop(Func<IStartLoopBuilder<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>, IFinalLoopBuilder<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>> loopBuilderTransformation) => Loop(loopBuilderTransformation);

        TTargetQuery IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Map<TTargetQuery>(Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, TTargetQuery> mapping) => Map(mapping);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Mute() => Mute();

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Not(Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IGremlinQueryBase> notTraversal) => Not(static (__, notTraversal) => notTraversal(__), notTraversal);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.None() => None();

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Optional(Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>> optionalTraversal) => Optional(optionalTraversal);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Or(params Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IGremlinQueryBase>[] orTraversals) => Or(orTraversals);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Order(Func<IOrderBuilder<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>, IOrderBuilderWithBy<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>> projection) => OrderGlobal(projection);
        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.OrderLocal(Func<IOrderBuilder<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>, IOrderBuilderWithBy<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>> projection) => OrderLocal(projection);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Order(Func<IOrderBuilder<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>, IOrderBuilderWithBy<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>> projection) => OrderGlobal(projection);
        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.OrderLocal(Func<IOrderBuilder<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>, IOrderBuilderWithBy<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>> projection) => OrderLocal(projection);

        IValueGremlinQuery<dynamic> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Project(Func<IProjectBuilder<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, TElement>, IProjectDynamicResult> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Project<TResult>(Func<IProjectBuilder<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, TElement>, IProjectMapResult<TResult>> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Project<TResult>(Func<IProjectBuilder<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, TElement>, IProjectTupleResult<TResult>> continuation) => Project<TResult>(continuation);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Range(long low, long high) => RangeGlobal(low, high);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.RangeLocal(long low, long high) => RangeLocal(low, high);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.SideEffect(Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IGremlinQueryBase> sideEffectTraversal) => SideEffect(sideEffectTraversal);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.SimplePath() => SimplePath();

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Skip(long count) => Skip(count, Scope.Global);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.SkipLocal(long count) => Skip(count, Scope.Local);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Tail(long count) => TailGlobal(count);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.TailLocal(long count) => TailLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Union<TTargetQuery>(params Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, TTargetQuery>[] unionTraversals) => Union<TTargetQuery, TTargetQuery>(unionTraversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Union(params Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IGremlinQueryBase>[] unionTraversals) => Union<IGremlinQueryBase, IValueGremlinQuery<object>>(unionTraversals);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);
        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Where(ILambda lambda) => Where(lambda);
        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Where(Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IGremlinQueryBase> filterTraversal) => Where(filterTraversal);

        TTargetQuery IGremlinQueryBaseRec<TElement, IPropertyGremlinQuery<TElement>>.Aggregate<TTargetQuery>(Func<IPropertyGremlinQuery<TElement>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IPropertyGremlinQuery<TElement>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Global, continuation);
        TTargetQuery IGremlinQueryBaseRec<TElement, IPropertyGremlinQuery<TElement>>.AggregateLocal<TTargetQuery>(Func<IPropertyGremlinQuery<TElement>, StepLabel<IArrayGremlinQuery<TElement[], TElement, IPropertyGremlinQuery<TElement>>, TElement[]>, TTargetQuery> continuation) => Aggregate(Scope.Local, continuation);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IPropertyGremlinQuery<TElement>>.Aggregate(StepLabel<IArrayGremlinQuery<TElement[], TElement, IPropertyGremlinQuery<TElement>>, TElement[]> stepLabel) => Aggregate(Scope.Global, stepLabel);
        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IPropertyGremlinQuery<TElement>>.AggregateLocal(StepLabel<IArrayGremlinQuery<TElement[], TElement, IPropertyGremlinQuery<TElement>>, TElement[]> stepLabel) => Aggregate(Scope.Local, stepLabel);

        TTargetQuery IGremlinQueryBaseRec<TElement, IPropertyGremlinQuery<TElement>>.As<TTargetQuery>(Func<IPropertyGremlinQuery<TElement>, StepLabel<IPropertyGremlinQuery<TElement>, TElement>, TTargetQuery> continuation) => As<StepLabel<IPropertyGremlinQuery<TElement>, TElement>, TTargetQuery>(continuation);
        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IPropertyGremlinQuery<TElement>>.As(StepLabel<TElement> stepLabel) => As(stepLabel);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.And(params Func<IPropertyGremlinQuery<TElement>, IGremlinQueryBase>[] andTraversals) => And(andTraversals);

        TTargetQuery IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.As<TTargetQuery>(Func<IPropertyGremlinQuery<TElement>, StepLabel<IPropertyGremlinQuery<TElement>, object>, TTargetQuery> continuation) => As<StepLabel<IPropertyGremlinQuery<TElement>, object>, TTargetQuery>(continuation);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Barrier() => Barrier();

        TTargetQuery IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Choose<TTargetQuery>(Func<IPropertyGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IPropertyGremlinQuery<TElement>, TTargetQuery> trueChoice, Func<IPropertyGremlinQuery<TElement>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(traversalPredicate, trueChoice, falseChoice);
        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Choose(Func<IPropertyGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IPropertyGremlinQuery<TElement>, IPropertyGremlinQuery<TElement>> trueChoice) => Choose<IPropertyGremlinQuery<TElement>, IPropertyGremlinQuery<TElement>, IPropertyGremlinQuery<TElement>>(traversalPredicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Choose(Func<IPropertyGremlinQuery<TElement>, IGremlinQueryBase> traversalPredicate, Func<IPropertyGremlinQuery<TElement>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(traversalPredicate, trueChoice);
        
        TTargetQuery IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Choose<TTargetQuery>(Func<IChooseBuilder<IPropertyGremlinQuery<TElement>>, IChooseBuilderWithCaseOrDefault<TTargetQuery>> continuation) => Choose<TTargetQuery>(continuation);

        TTargetQuery IGremlinQueryBaseRec<TElement, IPropertyGremlinQuery<TElement>>.Choose<TTargetQuery>(Expression<Func<TElement, bool>> predicate, Func<IPropertyGremlinQuery<TElement>, TTargetQuery> trueChoice, Func<IPropertyGremlinQuery<TElement>, TTargetQuery> falseChoice) => Choose<TTargetQuery, TTargetQuery, TTargetQuery>(predicate, trueChoice, falseChoice);
        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IPropertyGremlinQuery<TElement>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IPropertyGremlinQuery<TElement>, IPropertyGremlinQuery<TElement>> trueChoice) => Choose<IPropertyGremlinQuery<TElement>, IPropertyGremlinQuery<TElement>, IPropertyGremlinQuery<TElement>>(predicate, trueChoice);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<TElement, IPropertyGremlinQuery<TElement>>.Choose(Expression<Func<TElement, bool>> predicate, Func<IPropertyGremlinQuery<TElement>, IGremlinQueryBase> trueChoice) => Choose<IGremlinQueryBase, IGremlinQueryBase, IValueGremlinQuery<object>>(predicate, trueChoice);

        TTargetQuery IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Coalesce<TTargetQuery>(params Func<IPropertyGremlinQuery<TElement>, TTargetQuery>[] traversals) => Coalesce<TTargetQuery, TTargetQuery>(traversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Coalesce(params Func<IPropertyGremlinQuery<TElement>, IGremlinQueryBase>[] traversals) => Coalesce<IGremlinQueryBase, IValueGremlinQuery<object>>(traversals);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Coin(double probability) => Coin(probability);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.CyclicPath() => CyclicPath();

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Dedup() => DedupGlobal();

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.DedupLocal() => DedupLocal();

        TTargetQuery IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.FlatMap<TTargetQuery>(Func<IPropertyGremlinQuery<TElement>, TTargetQuery> mapping) => FlatMap(mapping);

        IArrayGremlinQuery<TElement[], TElement, IPropertyGremlinQuery<TElement>> IGremlinQueryBaseRec<TElement, IPropertyGremlinQuery<TElement>>.ForceArray() => CloneAs<IArrayGremlinQuery<TElement[], TElement, IPropertyGremlinQuery<TElement>>>();

        IArrayGremlinQuery<TElement[], TElement, IPropertyGremlinQuery<TElement>> IGremlinQueryBaseRec<TElement, IPropertyGremlinQuery<TElement>>.Fold() => Fold<IPropertyGremlinQuery<TElement>>();

        IValueGremlinQuery<IDictionary<TNewKey, TNewValue>> IGremlinQueryBaseRec<TElement, IPropertyGremlinQuery<TElement>>.Group<TNewKey, TNewValue>(Func<IGroupBuilder<TElement, IPropertyGremlinQuery<TElement>>, IGroupBuilderWithKeyAndValue<TNewKey, TNewValue>> groupBuilder) => Group(groupBuilder);
        IValueGremlinQuery<IDictionary<TNewKey, TElement[]>> IGremlinQueryBaseRec<TElement, IPropertyGremlinQuery<TElement>>.Group<TNewKey>(Func<IGroupBuilder<TElement, IPropertyGremlinQuery<TElement>>, IGroupBuilderWithKey<TElement, IPropertyGremlinQuery<TElement>, TNewKey>> groupBuilder) => Group(groupBuilder);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Identity() => Identity();

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IPropertyGremlinQuery<TElement>>.Inject(params TElement[] elements) => Inject(elements);
        
        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Limit(long count) => LimitGlobal(count);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.LimitLocal(long count) => LimitLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Local<TTargetQuery>(Func<IPropertyGremlinQuery<TElement> , TTargetQuery> localTraversal) => Local(localTraversal);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Loop(Func<IStartLoopBuilder<IPropertyGremlinQuery<TElement>>, IFinalLoopBuilder<IPropertyGremlinQuery<TElement>>> loopBuilderTransformation) => Loop(loopBuilderTransformation);

        TTargetQuery IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Map<TTargetQuery>(Func<IPropertyGremlinQuery<TElement>, TTargetQuery> mapping) => Map(mapping);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Mute() => Mute();

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Not(Func<IPropertyGremlinQuery<TElement>, IGremlinQueryBase> notTraversal) => Not(static (__, notTraversal) => notTraversal(__), notTraversal);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.None() => None();

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Optional(Func<IPropertyGremlinQuery<TElement>, IPropertyGremlinQuery<TElement>> optionalTraversal) => Optional(optionalTraversal);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Or(params Func<IPropertyGremlinQuery<TElement>, IGremlinQueryBase>[] orTraversals) => Or(orTraversals);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IPropertyGremlinQuery<TElement>>.Order(Func<IOrderBuilder<TElement, IPropertyGremlinQuery<TElement>>, IOrderBuilderWithBy<TElement, IPropertyGremlinQuery<TElement>>> projection) => OrderGlobal(projection);
        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IPropertyGremlinQuery<TElement>>.OrderLocal(Func<IOrderBuilder<TElement, IPropertyGremlinQuery<TElement>>, IOrderBuilderWithBy<TElement, IPropertyGremlinQuery<TElement>>> projection) => OrderLocal(projection);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Order(Func<IOrderBuilder<IPropertyGremlinQuery<TElement>>, IOrderBuilderWithBy<IPropertyGremlinQuery<TElement>>> projection) => OrderGlobal(projection);
        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.OrderLocal(Func<IOrderBuilder<IPropertyGremlinQuery<TElement>>, IOrderBuilderWithBy<IPropertyGremlinQuery<TElement>>> projection) => OrderLocal(projection);

        IValueGremlinQuery<dynamic> IGremlinQueryBaseRec<TElement, IPropertyGremlinQuery<TElement>>.Project(Func<IProjectBuilder<IPropertyGremlinQuery<TElement>, TElement>, IProjectDynamicResult> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IPropertyGremlinQuery<TElement>>.Project<TResult>(Func<IProjectBuilder<IPropertyGremlinQuery<TElement>, TElement>, IProjectMapResult<TResult>> continuation) => Project(continuation);
        IMapGremlinQuery<TResult> IGremlinQueryBaseRec<TElement, IPropertyGremlinQuery<TElement>>.Project<TResult>(Func<IProjectBuilder<IPropertyGremlinQuery<TElement>, TElement>, IProjectTupleResult<TResult>> continuation) => Project<TResult>(continuation);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Range(long low, long high) => RangeGlobal(low, high);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.RangeLocal(long low, long high) => RangeLocal(low, high);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.SideEffect(Func<IPropertyGremlinQuery<TElement>, IGremlinQueryBase> sideEffectTraversal) => SideEffect(sideEffectTraversal);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.SimplePath() => SimplePath();

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Skip(long count) => Skip(count, Scope.Global);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.SkipLocal(long count) => Skip(count, Scope.Local);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Tail(long count) => TailGlobal(count);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.TailLocal(long count) => TailLocal(count);

        TTargetQuery IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Union<TTargetQuery>(params Func<IPropertyGremlinQuery<TElement>, TTargetQuery>[] unionTraversals) => Union<TTargetQuery, TTargetQuery>(unionTraversals);
        IValueGremlinQuery<object> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Union(params Func<IPropertyGremlinQuery<TElement>, IGremlinQueryBase>[] unionTraversals) => Union<IGremlinQueryBase, IValueGremlinQuery<object>>(unionTraversals);

        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<TElement, IPropertyGremlinQuery<TElement>>.Where(Expression<Func<TElement, bool>> predicate) => Where(predicate);
        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Where(ILambda lambda) => Where(lambda);
        IPropertyGremlinQuery<TElement> IGremlinQueryBaseRec<IPropertyGremlinQuery<TElement>>.Where(Func<IPropertyGremlinQuery<TElement>, IGremlinQueryBase> filterTraversal) => Where(filterTraversal);



        IVertexGremlinQuery<TTarget> IVertexGremlinQueryBase.OfType<TTarget>() => OfType<TTarget>(Environment.Model.VerticesModel);
        IEdgeGremlinQuery<TTarget> IEdgeGremlinQueryBase.OfType<TTarget>() => OfType<TTarget>(Environment.Model.EdgesModel);


        IElementGremlinQuery<TElement> IElementGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Property(string key, object? value) => Property(key, value);
        IElementGremlinQuery<TElement> IElementGremlinQueryBaseRec<IElementGremlinQuery<TElement>>.Property(string key, Func<IElementGremlinQuery<TElement>, IGremlinQueryBase> valueTransformation) => Property(key, valueTransformation);

        IElementGremlinQuery<TElement> IElementGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, TProjectedValue value) => Property(projection, value);
        IElementGremlinQuery<TElement> IElementGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, StepLabel<TProjectedValue> stepLabel) => Property(projection, __ => __.Select(stepLabel));
        IElementGremlinQuery<TElement> IElementGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, Func<IElementGremlinQuery<TElement>, IGremlinQueryBase<TProjectedValue>> valueTraversal) => Property(projection, valueTraversal);

        IElementGremlinQuery<TElement> IElementGremlinQueryBaseRec<TElement, IElementGremlinQuery<TElement>>.Where<TProjection>(Expression<Func<TElement, TProjection>> projection, Func<IGremlinQueryBase<TProjection>, IGremlinQueryBase> propertyTraversal) => Where(projection, propertyTraversal);
        IEdgeOrVertexGremlinQuery<TElement> IElementGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Property(string key, object? value) => Property(key, value);
        IEdgeOrVertexGremlinQuery<TElement> IElementGremlinQueryBaseRec<IEdgeOrVertexGremlinQuery<TElement>>.Property(string key, Func<IEdgeOrVertexGremlinQuery<TElement>, IGremlinQueryBase> valueTransformation) => Property(key, valueTransformation);

        IEdgeOrVertexGremlinQuery<TElement> IElementGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, TProjectedValue value) => Property(projection, value);
        IEdgeOrVertexGremlinQuery<TElement> IElementGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, StepLabel<TProjectedValue> stepLabel) => Property(projection, __ => __.Select(stepLabel));
        IEdgeOrVertexGremlinQuery<TElement> IElementGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, Func<IEdgeOrVertexGremlinQuery<TElement>, IGremlinQueryBase<TProjectedValue>> valueTraversal) => Property(projection, valueTraversal);

        IEdgeOrVertexGremlinQuery<TElement> IElementGremlinQueryBaseRec<TElement, IEdgeOrVertexGremlinQuery<TElement>>.Where<TProjection>(Expression<Func<TElement, TProjection>> projection, Func<IGremlinQueryBase<TProjection>, IGremlinQueryBase> propertyTraversal) => Where(projection, propertyTraversal);
        IVertexGremlinQuery<TElement> IElementGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Property(string key, object? value) => Property(key, value);
        IVertexGremlinQuery<TElement> IElementGremlinQueryBaseRec<IVertexGremlinQuery<TElement>>.Property(string key, Func<IVertexGremlinQuery<TElement>, IGremlinQueryBase> valueTransformation) => Property(key, valueTransformation);

        IVertexGremlinQuery<TElement> IElementGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, TProjectedValue value) => Property(projection, value);
        IVertexGremlinQuery<TElement> IElementGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, StepLabel<TProjectedValue> stepLabel) => Property(projection, __ => __.Select(stepLabel));
        IVertexGremlinQuery<TElement> IElementGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, Func<IVertexGremlinQuery<TElement>, IGremlinQueryBase<TProjectedValue>> valueTraversal) => Property(projection, valueTraversal);

        IVertexGremlinQuery<TElement> IElementGremlinQueryBaseRec<TElement, IVertexGremlinQuery<TElement>>.Where<TProjection>(Expression<Func<TElement, TProjection>> projection, Func<IGremlinQueryBase<TProjection>, IGremlinQueryBase> propertyTraversal) => Where(projection, propertyTraversal);
        IEdgeGremlinQuery<TElement> IElementGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Property(string key, object? value) => Property(key, value);
        IEdgeGremlinQuery<TElement> IElementGremlinQueryBaseRec<IEdgeGremlinQuery<TElement>>.Property(string key, Func<IEdgeGremlinQuery<TElement>, IGremlinQueryBase> valueTransformation) => Property(key, valueTransformation);

        IEdgeGremlinQuery<TElement> IElementGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, TProjectedValue value) => Property(projection, value);
        IEdgeGremlinQuery<TElement> IElementGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, StepLabel<TProjectedValue> stepLabel) => Property(projection, __ => __.Select(stepLabel));
        IEdgeGremlinQuery<TElement> IElementGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, Func<IEdgeGremlinQuery<TElement>, IGremlinQueryBase<TProjectedValue>> valueTraversal) => Property(projection, valueTraversal);

        IEdgeGremlinQuery<TElement> IElementGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement>>.Where<TProjection>(Expression<Func<TElement, TProjection>> projection, Func<IGremlinQueryBase<TProjection>, IGremlinQueryBase> propertyTraversal) => Where(projection, propertyTraversal);
        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IElementGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Property(string key, object? value) => Property(key, value);
        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IElementGremlinQueryBaseRec<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Property(string key, Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase> valueTransformation) => Property(key, valueTransformation);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IElementGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, TProjectedValue value) => Property(projection, value);
        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IElementGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, StepLabel<TProjectedValue> stepLabel) => Property(projection, __ => __.Select(stepLabel));
        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IElementGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, Func<IInOrOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase<TProjectedValue>> valueTraversal) => Property(projection, valueTraversal);

        IInOrOutEdgeGremlinQuery<TElement, TOutVertex> IElementGremlinQueryBaseRec<TElement, IInOrOutEdgeGremlinQuery<TElement, TOutVertex>>.Where<TProjection>(Expression<Func<TElement, TProjection>> projection, Func<IGremlinQueryBase<TProjection>, IGremlinQueryBase> propertyTraversal) => Where(projection, propertyTraversal);
        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IElementGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Property(string key, object? value) => Property(key, value);
        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IElementGremlinQueryBaseRec<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Property(string key, Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IGremlinQueryBase> valueTransformation) => Property(key, valueTransformation);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IElementGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, TProjectedValue value) => Property(projection, value);
        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IElementGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, StepLabel<TProjectedValue> stepLabel) => Property(projection, __ => __.Select(stepLabel));
        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IElementGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, Func<IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>, IGremlinQueryBase<TProjectedValue>> valueTraversal) => Property(projection, valueTraversal);

        IEdgeGremlinQuery<TElement, TOutVertex, TInVertex> IElementGremlinQueryBaseRec<TElement, IEdgeGremlinQuery<TElement, TOutVertex, TInVertex>>.Where<TProjection>(Expression<Func<TElement, TProjection>> projection, Func<IGremlinQueryBase<TProjection>, IGremlinQueryBase> propertyTraversal) => Where(projection, propertyTraversal);
        IInEdgeGremlinQuery<TElement, TInVertex> IElementGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Property(string key, object? value) => Property(key, value);
        IInEdgeGremlinQuery<TElement, TInVertex> IElementGremlinQueryBaseRec<IInEdgeGremlinQuery<TElement, TInVertex>>.Property(string key, Func<IInEdgeGremlinQuery<TElement, TInVertex>, IGremlinQueryBase> valueTransformation) => Property(key, valueTransformation);

        IInEdgeGremlinQuery<TElement, TInVertex> IElementGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, TProjectedValue value) => Property(projection, value);
        IInEdgeGremlinQuery<TElement, TInVertex> IElementGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, StepLabel<TProjectedValue> stepLabel) => Property(projection, __ => __.Select(stepLabel));
        IInEdgeGremlinQuery<TElement, TInVertex> IElementGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, Func<IInEdgeGremlinQuery<TElement, TInVertex>, IGremlinQueryBase<TProjectedValue>> valueTraversal) => Property(projection, valueTraversal);

        IInEdgeGremlinQuery<TElement, TInVertex> IElementGremlinQueryBaseRec<TElement, IInEdgeGremlinQuery<TElement, TInVertex>>.Where<TProjection>(Expression<Func<TElement, TProjection>> projection, Func<IGremlinQueryBase<TProjection>, IGremlinQueryBase> propertyTraversal) => Where(projection, propertyTraversal);
        IOutEdgeGremlinQuery<TElement, TOutVertex> IElementGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Property(string key, object? value) => Property(key, value);
        IOutEdgeGremlinQuery<TElement, TOutVertex> IElementGremlinQueryBaseRec<IOutEdgeGremlinQuery<TElement, TOutVertex>>.Property(string key, Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase> valueTransformation) => Property(key, valueTransformation);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IElementGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, TProjectedValue value) => Property(projection, value);
        IOutEdgeGremlinQuery<TElement, TOutVertex> IElementGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, StepLabel<TProjectedValue> stepLabel) => Property(projection, __ => __.Select(stepLabel));
        IOutEdgeGremlinQuery<TElement, TOutVertex> IElementGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, Func<IOutEdgeGremlinQuery<TElement, TOutVertex>, IGremlinQueryBase<TProjectedValue>> valueTraversal) => Property(projection, valueTraversal);

        IOutEdgeGremlinQuery<TElement, TOutVertex> IElementGremlinQueryBaseRec<TElement, IOutEdgeGremlinQuery<TElement, TOutVertex>>.Where<TProjection>(Expression<Func<TElement, TProjection>> projection, Func<IGremlinQueryBase<TProjection>, IGremlinQueryBase> propertyTraversal) => Where(projection, propertyTraversal);
        IVertexPropertyGremlinQuery<TElement, TScalar> IElementGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Property(string key, object? value) => Property(key, value);
        IVertexPropertyGremlinQuery<TElement, TScalar> IElementGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar>>.Property(string key, Func<IVertexPropertyGremlinQuery<TElement, TScalar>, IGremlinQueryBase> valueTransformation) => Property(key, valueTransformation);

        IVertexPropertyGremlinQuery<TElement, TScalar> IElementGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, TProjectedValue value) => Property(projection, value);
        IVertexPropertyGremlinQuery<TElement, TScalar> IElementGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, StepLabel<TProjectedValue> stepLabel) => Property(projection, __ => __.Select(stepLabel));
        IVertexPropertyGremlinQuery<TElement, TScalar> IElementGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, Func<IVertexPropertyGremlinQuery<TElement, TScalar>, IGremlinQueryBase<TProjectedValue>> valueTraversal) => Property(projection, valueTraversal);

        IVertexPropertyGremlinQuery<TElement, TScalar> IElementGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar>>.Where<TProjection>(Expression<Func<TElement, TProjection>> projection, Func<IGremlinQueryBase<TProjection>, IGremlinQueryBase> propertyTraversal) => Where(projection, propertyTraversal);
        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IElementGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Property(string key, object? value) => Property(key, value);
        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IElementGremlinQueryBaseRec<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Property(string key, Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IGremlinQueryBase> valueTransformation) => Property(key, valueTransformation);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IElementGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, TProjectedValue value) => Property(projection, value);
        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IElementGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, StepLabel<TProjectedValue> stepLabel) => Property(projection, __ => __.Select(stepLabel));
        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IElementGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Property<TProjectedValue>(Expression<Func<TElement, TProjectedValue>> projection, Func<IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>, IGremlinQueryBase<TProjectedValue>> valueTraversal) => Property(projection, valueTraversal);

        IVertexPropertyGremlinQuery<TElement, TScalar, TMeta> IElementGremlinQueryBaseRec<TElement, IVertexPropertyGremlinQuery<TElement, TScalar, TMeta>>.Where<TProjection>(Expression<Func<TElement, TProjection>> projection, Func<IGremlinQueryBase<TProjection>, IGremlinQueryBase> propertyTraversal) => Where(projection, propertyTraversal);
   }
}

