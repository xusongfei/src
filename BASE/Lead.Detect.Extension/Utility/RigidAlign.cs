using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Lead.Detect.PlatformCalibration
{
    public class RigidAlign
    {

        #region affine transform
        public static double[] AffineTransform(double[] pos, double[,] affine)
        {
            var mat = DenseMatrix.OfArray(new double[,]
            {
                {affine[0, 0], affine[0, 1], affine[0, 3]},
                {affine[1, 0], affine[1, 1], affine[1, 3]}
            });

            return mat.Multiply(DenseVector.OfArray(new[] { pos[0], pos[1], 1 })).ToArray();
        }

        public static double[] AffineInverseTransform(double[] pos, double[,] affine)
        {
            var mat = DenseMatrix.OfArray(new double[,]
            {
                {affine[0, 0], affine[0, 1], affine[0, 3]},
                {affine[1, 0], affine[1, 1], affine[1, 3]},
                {0, 0, 1}
            }).Inverse();

            var newMat = DenseMatrix.OfArray(new double[,]
            {
                {mat[0, 0], mat[0, 1], mat[0, 2]},
                {mat[1, 0], mat[1, 1], mat[1, 2]},
            });

            return newMat.Multiply(DenseVector.OfArray(new[] { pos[0], pos[1], 1 })).ToArray();
        }


        public static Tuple<double[,], double> AffineAlign(double[] sx, double[] sy, double[] tx, double[] ty)
        {
            List<Vector<double>> src = new List<Vector<double>>();
            for (int i = 0; i < sx.Length; i++)
            {
                src.Add(DenseVector.OfArray(new[] { sx[i], sy[i], 1 }));
            }

            var a = DenseMatrix.OfRowVectors(src);

            var vec1 = a.TransposeThisAndMultiply(a).Inverse().Multiply(a.Transpose()).Multiply(DenseVector.OfArray(tx));
            var vec2 = a.TransposeThisAndMultiply(a).Inverse().Multiply(a.Transpose()).Multiply(DenseVector.OfArray(ty));
            var affine = DenseMatrix.OfRowVectors(vec1, vec2);


            //calculate errors
            List<double> errors = new List<double>();
            for (int i = 0; i < src.Count; i++)
            {
                errors.Add((affine * src[i] - DenseVector.OfArray(new[] { tx[i], ty[i] })).PointwisePower(2).Norm(1));
            }

            return new Tuple<double[,], double>(new double[,]
                {
                    {affine[0, 0], affine[0, 1], 0, affine[0, 2]},
                    {affine[1, 0], affine[1, 1], 0, affine[1, 2]},
                    {0, 0, 1, 0},
                    {0, 0, 0, 1},
                },
                errors.Max());
        }

        #endregion


        #region rigid align 3

        /// <summary>
        /// 2d transform
        /// </summary>
        /// <param name="src"></param>
        /// <param name="align"></param>
        /// <returns></returns>
        public static double[] Transform(double[] src, double[,] align)
        {
            var scale = align[2, 0];
            var r = DenseMatrix.OfArray(new double[,] { { align[0, 0], align[0, 1] }, { align[1, 0], align[1, 1] } });
            var t = DenseVector.OfArray(new double[] { align[0, 2], align[1, 2] });
            return (scale * r * DenseVector.OfArray(src.Take(2).ToArray()) + t).ToArray();
        }

        public static double[] Transform3(double[] src, double[,] align)
        {
            var scale = align[3, 0];
            var r = DenseMatrix.OfArray(new double[,] { { align[0, 0], align[0, 1], align[0, 2] }, { align[1, 0], align[1, 1], align[1, 2] }, { align[2, 0], align[2, 1], align[2, 2] } });
            var t = DenseVector.OfArray(new double[] { align[0, 3], align[1, 3], align[2, 3] });
            return (scale * r * DenseVector.OfArray(src.Take(3).ToArray()) + t).ToArray();
        }


        ///// <summary>
        ///// up to 3 dimension scale, rotation, translation
        ///// </summary>
        ///// <param name="src"></param>
        ///// <param name="align"></param>
        ///// <returns></returns>
        //public static Vector<double> Transform(Vector<double> src, Tuple<Matrix<double>, double, Vector<double>> align)
        //{
        //    return align.Item2 * align.Item1 * src + align.Item3;
        //}


        /// <summary>
        /// rigid align matrix calculate
        /// </summary>
        /// <param name="sx"></param>
        /// <param name="sy"></param>
        /// <param name="tx"></param>
        /// <param name="ty"></param>
        /// <returns></returns>
        public static Tuple<double[,], double> Align(double[] sx, double[] sy, double[] tx, double[] ty)
        {
            List<Vector<double>> src = new List<Vector<double>>();
            for (int i = 0; i < sx.Length; i++)
            {
                src.Add(DenseVector.OfArray(new[] { sx[i], sy[i] }));
            }

            List<Vector<double>> tar = new List<Vector<double>>();
            for (int i = 0; i < tx.Length; i++)
            {
                tar.Add(DenseVector.OfArray(new[] { tx[i], ty[i] }));
            }

            var ret = Align(src, tar);

            var mat = DenseMatrix.Create(3, 3, 0);

            //r
            mat[0, 0] = ret.Item1[0, 0];
            mat[0, 1] = ret.Item1[0, 1];
            mat[1, 0] = ret.Item1[1, 0];
            mat[1, 1] = ret.Item1[1, 1];

            //vt
            mat[0, 2] = ret.Item3[0];
            mat[1, 2] = ret.Item3[1];

            //scale
            mat[2, 0] = ret.Item2;
            mat[2, 1] = ret.Item2;

            mat[2, 2] = 1;

            return new Tuple<double[,], double>(mat.ToArray(), ret.Item4.Max());
        }

        public static Tuple<double[,], double> Align3(double[] sx, double[] sy, double[] sz, double[] tx, double[] ty, double[] tz)
        {
            List<Vector<double>> src = new List<Vector<double>>();
            for (int i = 0; i < sx.Length; i++)
            {
                src.Add(DenseVector.OfArray(new[] { sx[i], sy[i], sz[i] }));
            }

            List<Vector<double>> tar = new List<Vector<double>>();
            for (int i = 0; i < tx.Length; i++)
            {
                tar.Add(DenseVector.OfArray(new[] { tx[i], ty[i], tz[i] }));
            }

            var ret = Align(src, tar);

            var mat = DenseMatrix.Create(4, 4, 0);

            //r
            mat[0, 0] = ret.Item1[0, 0];
            mat[0, 1] = ret.Item1[0, 1];
            mat[0, 2] = ret.Item1[0, 2];
            mat[1, 0] = ret.Item1[1, 0];
            mat[1, 1] = ret.Item1[1, 1];
            mat[1, 2] = ret.Item1[1, 2];
            mat[2, 0] = ret.Item1[2, 0];
            mat[2, 1] = ret.Item1[2, 1];
            mat[2, 2] = ret.Item1[2, 2];

            //vt
            mat[0, 3] = ret.Item3[0];
            mat[1, 3] = ret.Item3[1];
            mat[2, 3] = ret.Item3[2];

            //scale
            mat[3, 0] = ret.Item2;
            mat[3, 1] = ret.Item2;
            mat[3, 2] = ret.Item2;

            mat[3, 3] = 1;

            return new Tuple<double[,], double>(mat.ToArray(), ret.Item4.Max());
        }


        /// <summary>
        ///  calc transform for single scale parameter
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Tuple<Matrix<double>, double, Vector<double>, List<double>> Align(List<Vector<double>> origin, List<Vector<double>> target)
        {
            var x = DenseMatrix.OfColumnVectors(origin);
            var y = DenseMatrix.OfColumnVectors(target);

            var mx = x.RowSums() / x.ColumnCount;
            var my = y.RowSums() / y.ColumnCount;

            var xc = x - DenseMatrix.OfColumnVectors(Enumerable.Repeat(mx, x.ColumnCount));
            var yc = y - DenseMatrix.OfColumnVectors(Enumerable.Repeat(my, x.ColumnCount));

            var sx = (xc.PointwisePower(2)).RowNorms(1).Sum() / xc.ColumnCount;
            var sy = (yc.PointwisePower(2)).RowNorms(1).Sum() / yc.ColumnCount;

            var Sxy = yc * xc.Transpose() / xc.ColumnCount;

            var svd = Sxy.Svd();
            var U = svd.U;
            var D = svd.W;
            var VT = svd.VT;

            var rankSxy = Sxy.Rank();
            var detSxy = Sxy.Determinant();
            var S = DiagonalMatrix.CreateIdentity(x.RowCount);

            if (rankSxy > x.ColumnCount - 1)
            {
                if (detSxy < 0)
                {
                    S[x.RowCount, x.RowCount] = -1;
                }
                else if (rankSxy == x.RowCount - 1)
                {
                    if (U.Determinant() * VT.Determinant() < 0)
                    {
                        S[x.RowCount, x.RowCount] = -1;
                    }
                }
                else
                {
                    var r = DiagonalMatrix.CreateIdentity(origin.First().Count);
                    var c = 1;
                    var t = DenseVector.OfArray(new Double[origin.First().Count]);

                    return new Tuple<Matrix<double>, double, Vector<double>, List<double>>(r, c, t, new List<double>() { 0d });
                }
            }

            var R = U * S * VT;
            var C = (D * S).Trace() / sx;
            var T = my - C * R * mx;


            //calculate errors
            List<double> errors = new List<double>();
            for (int i = 0; i < origin.Count; i++)
            {
                errors.Add((target[i] - (C * R * origin[i] + T)).PointwisePower(2).Norm(1));
            }


            return new Tuple<Matrix<double>, double, Vector<double>, List<double>>(R, C, T, errors);
        }


        public static string AlignTest(double[] sx, double[] sy, double[] tx, double[] ty)
        {
            List<Vector<double>> src = new List<Vector<double>>();
            for (int i = 0; i < sx.Length; i++)
            {
                src.Add(DenseVector.OfArray(new[] { sx[i], sy[i] }));
            }

            List<Vector<double>> tar = new List<Vector<double>>();
            for (int i = 0; i < tx.Length; i++)
            {
                tar.Add(DenseVector.OfArray(new[] { tx[i], ty[i] }));
            }

            var ret = Align(src, tar);


            StringBuilder sb = new StringBuilder();

            sb.AppendLine(ret.Item1.ToMatrixString());
            sb.AppendLine(ret.Item2.ToString("F6"));
            sb.AppendLine(ret.Item3.ToVectorString());

            return sb.ToString();
        }

        #endregion

        #region rigid align 2

        /// <summary>
        /// up to 3 dimension scale, rotation, translation
        /// </summary>
        /// <param name="src"></param>
        /// <param name="align2"></param>
        /// <returns></returns>
        public static Vector<double> Transform2(Vector<double> src, Tuple<Matrix<double>, Vector<double>, Vector<double>> align2)
        {
            return DiagonalMatrix.OfDiagonal(2, 2, align2.Item2) * align2.Item1 * src + align2.Item3;
        }

        /// <summary>
        /// align2 transform calculation, todo to seperate scale parameter in each dimension
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Tuple<Matrix<double>, Vector<double>, Vector<double>> Align2(List<Vector<double>> origin, List<Vector<double>> target)
        {
            //convert to m x n matrix, m - dimension, n - points count
            var x = DenseMatrix.OfColumnVectors(origin);
            var y = DenseMatrix.OfColumnVectors(target);

            //mean
            var mx = x.RowSums() / x.ColumnCount;
            var my = y.RowSums() / y.ColumnCount;

            //centered matrix
            var xc = x - DenseMatrix.OfColumnVectors(Enumerable.Repeat(mx, x.ColumnCount));
            var yc = y - DenseMatrix.OfColumnVectors(Enumerable.Repeat(my, x.ColumnCount));

            //scale 
            var sx = (xc.PointwisePower(2)).RowNorms(1) / xc.ColumnCount;
            var sy = (yc.PointwisePower(2)).RowNorms(1) / yc.ColumnCount;

            var Sxy = yc * xc.Transpose() / xc.ColumnCount;

            var svd = Sxy.Svd();
            var U = svd.U;
            var D = svd.W;
            var VT = svd.VT;

            var rankSxy = Sxy.Rank();
            var detSxy = Sxy.Determinant();
            var S = DiagonalMatrix.CreateIdentity(x.RowCount);

            if (rankSxy > x.ColumnCount - 1)
            {
                if (detSxy < 0)
                {
                    S[x.RowCount, x.RowCount] = -1;
                }
                else if (rankSxy == x.RowCount - 1)
                {
                    if (U.Determinant() * VT.Determinant() < 0)
                    {
                        S[x.RowCount, x.RowCount] = -1;
                    }
                }
                else
                {
                    var r = DiagonalMatrix.CreateIdentity(2);
                    var c = DenseVector.OfArray(new[] { 1d, 1d });
                    var t = DenseVector.OfArray(new[] { 0d, 0d });

                    return new Tuple<Matrix<double>, Vector<double>, Vector<double>>(r, c, t);
                }
            }

            var R = U * S * VT;
            var C = (D.Diagonal() * S) / sx;
            var Cinv = (D.Diagonal() * S) / sy;
            var T = my - C * R * mx;

            return new Tuple<Matrix<double>, Vector<double>, Vector<double>>(R, C, T);
        }


        public static string Align2Test(double[] sx, double[] sy, double[] tx, double[] ty)
        {
            List<Vector<double>> src = new List<Vector<double>>();
            for (int i = 0; i < sx.Length; i++)
            {
                src.Add(DenseVector.OfArray(new[] { sx[i], sy[i] }));
            }

            List<Vector<double>> tar = new List<Vector<double>>();
            for (int i = 0; i < tx.Length; i++)
            {
                tar.Add(DenseVector.OfArray(new[] { tx[i], ty[i] }));
            }

            var ret = Align2(src, tar);


            StringBuilder sb = new StringBuilder();
            sb.AppendLine(ret.Item1.ToMatrixString());
            sb.AppendLine(ret.Item2.ToString("F6"));
            sb.AppendLine(ret.Item3.ToVectorString());
            return sb.ToString();
        }

        #endregion

        #region reference

        private void CppCode()
        {
            //        typedef Eigen::Matrix < double, 3, 1, Eigen::DontAlign > Vector3d_U; // microsoft's 32-bit compiler can't put Eigen::Vector3d inside a std::vector. for other compilers or for 64-bit, feel free to replace this by Eigen::Vector3d
            //        /**
            //         *  @brief rigidly aligns two sets of poses
            //         *
            //         *  This calculates such a relative pose <tt>R, t</tt>, such that:
            //         *
            //         *  @code
            //         *  _TyVector v_pose = R * r_vertices[i] + t;
            //         *  double f_error = (r_tar_vertices[i] - v_pose).squaredNorm();
            //         *  @endcode
            //         *
            //         *  The sum of squared errors in <tt>f_error</tt> for each <tt>i</tt> is minimized.
            //         *
            //         *  @param[in] r_vertices is a set of vertices to be aligned
            //         *  @param[in] r_tar_vertices is a set of vertices to align to
            //         *
            //         *  @return Returns a relative pose that rigidly aligns the two given sets of poses.
            //         *
            //         *  @note This requires the two sets of poses to have the corresponding vertices stored under the same index.
            //         */
            //        static std::pair<Eigen::Matrix3d, Eigen::Vector3d> t_Align_Points(
            //    const std::vector<Vector3d_U> &r_vertices, const std::vector<Vector3d_U> &r_tar_vertices)
            //{
            //            _ASSERTE(r_tar_vertices.size() == r_vertices.size());
            //            const size_t n = r_vertices.size();
            //            Eigen::Vector3d v_center_tar3 = Eigen::Vector3d::Zero(), v_center3 = Eigen::Vector3d::Zero();
            //            for (size_t i = 0; i < n; ++i)
            //            {
            //                v_center_tar3 += r_tar_vertices[i];
            //                v_center3 += r_vertices[i];
            //            }
            //            v_center_tar3 /= double(n);
            //            v_center3 /= double(n);
            //            // calculate centers of positions, potentially extend to 3D
            //            double f_sd2_tar = 0, f_sd2 = 0; // only one of those is really needed
            //            Eigen::Matrix3d t_cov = Eigen::Matrix3d::Zero();
            //            for (size_t i = 0; i < n; ++i)
            //            {
            //                Eigen::Vector3d v_vert_i_tar = r_tar_vertices[i] - v_center_tar3;
            //                Eigen::Vector3d v_vert_i = r_vertices[i] - v_center3;
            //                // get both vertices
            //                f_sd2 += v_vert_i.squaredNorm();
            //                f_sd2_tar += v_vert_i_tar.squaredNorm();
            //                // accumulate squared standard deviation (only one of those is really needed)
            //                t_cov.noalias() += v_vert_i * v_vert_i_tar.transpose();
            //                // accumulate covariance
            //            }
            //            // calculate the covariance matrix
            //            Eigen::JacobiSVD<Eigen::Matrix3d> svd(t_cov, Eigen::ComputeFullU | Eigen::ComputeFullV);
            //            // calculate the SVD
            //            Eigen::Matrix3d R = svd.matrixV() * svd.matrixU().transpose();
            //            // compute the rotation
            //            double f_det = R.determinant();
            //            Eigen::Vector3d e(1, 1, (f_det < 0) ? -1 : 1);
            //            // calculate determinant of V*U^T to disambiguate rotation sign
            //            if (f_det < 0)
            //                R.noalias() = svd.matrixV() * e.asDiagonal() * svd.matrixU().transpose();
            //            // recompute the rotation part if the determinant was negative
            //            R = Eigen::Quaterniond(R).normalized().toRotationMatrix();
            //            // renormalize the rotation (not needed but gives slightly more orthogonal transformations)
            //            double f_scale = svd.singularValues().dot(e) / f_sd2_tar;
            //            double f_inv_scale = svd.singularValues().dot(e) / f_sd2; // only one of those is needed
            //                                                                      // calculate the scale
            //            R *= f_inv_scale;
            //            // apply scale
            //            Eigen::Vector3d t = v_center_tar3 - (R * v_center3); // R needs to contain scale here, otherwise the translation is wrong
            //                                                                 // want to align center with ground truth
            //            return std::make_pair(R, t); // or put it in a single 4x4 matrix if you like
            //        }
        }

        private void Eigen()
        {
            //            // This file is part of Eigen, a lightweight C++ template library
            //            // for linear algebra.
            //            //
            //            // Copyright (C) 2009 Hauke Heibel <hauke.heibel@gmail.com>
            //            //
            //            // This Source Code Form is subject to the terms of the Mozilla
            //            // Public License v. 2.0. If a copy of the MPL was not distributed
            //            // with this file, You can obtain one at http://mozilla.org/MPL/2.0/.
            //# ifndef EIGEN_UMEYAMA_H
            //#define EIGEN_UMEYAMA_H
            //            // This file requires the user to include 
            //            // * Eigen/Core
            //            // * Eigen/LU 
            //            // * Eigen/SVD
            //            // * Eigen/Array
            //    namespace Eigen
            //    {
            //# ifndef EIGEN_PARSED_BY_DOXYGEN
            //        // These helpers are required since it allows to use mixed types as parameters
            //        // for the Umeyama. The problem with mixed parameters is that the return type
            //        // cannot trivially be deduced when float and double types are mixed.
            //        namespace internal {
            //// Compile time return type deduction for different MatrixBase types.
            //// Different means here different alignment and parameters but the same underlying
            //// real scalar type.
            //template<typename MatrixType, typename OtherMatrixType>
            //        struct umeyama_transform_matrix_type
            //        {
            //            enum {
            //                MinRowsAtCompileTime = EIGEN_SIZE_MIN_PREFER_DYNAMIC(MatrixType::RowsAtCompileTime, OtherMatrixType::RowsAtCompileTime),
            //                // When possible we want to choose some small fixed size value since the result
            //                // is likely to fit on the stack. So here, EIGEN_SIZE_MIN_PREFER_DYNAMIC is not what we want.
            //                HomogeneousDimension = int(MinRowsAtCompileTime) == Dynamic ? Dynamic : int(MinRowsAtCompileTime) + 1
            //            };
            //            typedef Matrix<typename traits<MatrixType>::Scalar,
            //              HomogeneousDimension,
            //              HomogeneousDimension,
            //              AutoAlign | (traits<MatrixType>::Flags & RowMajorBit? RowMajor : ColMajor),
            //    HomogeneousDimension,
            //    HomogeneousDimension
            //  > type;
            //};
            //        }
            //#endif
            //        /**
            //        * \geometry_module \ingroup Geometry_Module
            //        *
            //        * \brief Returns the transformation between two point sets.
            //        *
            //        * The algorithm is based on:
            //        * "Least-squares estimation of transformation parameters between two point patterns",
            //        * Shinji Umeyama, PAMI 1991, DOI: 10.1109/34.88573
            //        *
            //        * It estimates parameters \f$ c, \mathbf{R}, \f$ and \f$ \mathbf{t} \f$ such that
            //        * \f{align*}
            //        *   \frac{1}{n} \sum_{i=1}^n \vert\vert y_i - (c\mathbf{R}x_i + \mathbf{t}) \vert\vert_2^2
            //        * \f}
            //        * is minimized.
            //        *
            //        * The algorithm is based on the analysis of the covariance matrix
            //        * \f$ \Sigma_{\mathbf{x}\mathbf{y}} \in \mathbb{R}^{d \times d} \f$
            //        * of the input point sets \f$ \mathbf{x} \f$ and \f$ \mathbf{y} \f$ where 
            //        * \f$d\f$ is corresponding to the dimension (which is typically small).
            //        * The analysis is involving the SVD having a complexity of \f$O(d^3)\f$
            //        * though the actual computational effort lies in the covariance
            //        * matrix computation which has an asymptotic lower bound of \f$O(dm)\f$ when 
            //        * the input point sets have dimension \f$d \times m\f$.
            //        *
            //        * Currently the method is working only for floating point matrices.
            //        *
            //        * \todo Should the return type of umeyama() become a Transform?
            //        *
            //        * \param src Source points \f$ \mathbf{x} = \left( x_1, \hdots, x_n \right) \f$.
            //        * \param dst Destination points \f$ \mathbf{y} = \left( y_1, \hdots, y_n \right) \f$.
            //        * \param with_scaling Sets \f$ c=1 \f$ when <code>false</code> is passed.
            //        * \return The homogeneous transformation 
            //        * \f{align*}
            //        *   T = \begin{bmatrix} c\mathbf{R} & \mathbf{t} \\ \mathbf{0} & 1 \end{bmatrix}
            //        * \f}
            //        * minimizing the resudiual above. This transformation is always returned as an 
            //        * Eigen::Matrix.
            //*/
            //        template<typename Derived, typename OtherDerived>
            //        typename internal::umeyama_transform_matrix_type<Derived, OtherDerived>::type
            //        umeyama(const MatrixBase<Derived>& src, const MatrixBase<OtherDerived>& dst, bool with_scaling = true)
            //        {
            //            typedef typename internal::umeyama_transform_matrix_type<Derived, OtherDerived>::type TransformationMatrixType;
            //        typedef typename internal::traits<TransformationMatrixType>::Scalar Scalar;
            //        typedef typename NumTraits<Scalar>::Real RealScalar;
            //  EIGEN_STATIC_ASSERT(!NumTraits<Scalar>::IsComplex, NUMERIC_TYPE_MUST_BE_REAL)
            //  EIGEN_STATIC_ASSERT((internal::is_same<Scalar, typename internal::traits<OtherDerived>::Scalar>::value),
            //    YOU_MIXED_DIFFERENT_NUMERIC_TYPES__YOU_NEED_TO_USE_THE_CAST_METHOD_OF_MATRIXBASE_TO_CAST_NUMERIC_TYPES_EXPLICITLY)
            //  enum { Dimension = EIGEN_SIZE_MIN_PREFER_DYNAMIC(Derived::RowsAtCompileTime, OtherDerived::RowsAtCompileTime) };
            //        typedef Matrix<Scalar, Dimension, 1> VectorType;
            //        typedef Matrix<Scalar, Dimension, Dimension> MatrixType;
            //        typedef typename internal::plain_matrix_type_row_major<Derived>::type RowMajorMatrixType;

            //        const Index m = src.rows(); // dimension
            //        const Index n = src.cols(); // number of measurements

            //        // required for demeaning ...
            //        const RealScalar one_over_n = RealScalar(1) / static_cast<RealScalar>(n);

            //        // computation of mean
            //        const VectorType src_mean = src.rowwise().sum() * one_over_n;
            //        const VectorType dst_mean = dst.rowwise().sum() * one_over_n;

            //        // demeaning of src and dst points
            //        const RowMajorMatrixType src_demean = src.colwise() - src_mean;
            //        const RowMajorMatrixType dst_demean = dst.colwise() - dst_mean;

            //        // Eq. (36)-(37)
            //        const Scalar src_var = src_demean.rowwise().squaredNorm().sum() * one_over_n;

            //        // Eq. (38)
            //        const MatrixType sigma = one_over_n * dst_demean * src_demean.transpose();

            //        JacobiSVD<MatrixType> svd(sigma, ComputeFullU | ComputeFullV);

            //        // Initialize the resulting transformation with an identity matrix...
            //        TransformationMatrixType Rt = TransformationMatrixType::Identity(m + 1, m + 1);

            //        // Eq. (39)
            //        VectorType S = VectorType::Ones(m);

            //  if  ( svd.matrixU().determinant() * svd.matrixV().determinant() < 0 )
            //    S(m-1) = -1;

            //  // Eq. (40) and (43)
            //  Rt.block(0,0,m,m).noalias() = svd.matrixU() * S.asDiagonal() * svd.matrixV().transpose();

            //  if (with_scaling)
            //  {
            //    // Eq. (42)
            //    const Scalar c = Scalar(1) / src_var * svd.singularValues().dot(S);

            //        // Eq. (41)
            //        Rt.col(m).head(m) = dst_mean;
            //    Rt.col(m).head(m).noalias() -= c* Rt.topLeftCorner(m, m)* src_mean;
            //        Rt.block(0,0,m,m) *= c;
            //  }
            //  else
            //  {
            //    Rt.col(m).head(m) = dst_mean;
            //    Rt.col(m).head(m).noalias() -= Rt.topLeftCorner(m,m)* src_mean;
            //}

            //  return Rt;
            //}

            //} // end namespace Eigen

            //#endif // EIGEN_UMEYAMA_H
        }

        #endregion
    }
}