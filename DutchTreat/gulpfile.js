/// <binding AfterBuild='default' />
/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var uglify = require('gulp-uglify');
var concat = require('gulp-concat');

gulp.task('minify', function () {
  // js files in js directory and all subdirectories
  return gulp.src('wwwroot/js/**/*.js')
    //fluent syntax - minify each file
    .pipe(uglify())
    // concatenate all minified files into one bundle
    .pipe(concat('dutchtreat.min.js'))
    .pipe(gulp.dest('wwwroot/dist'));
});

gulp.task('default', ["minify"]);