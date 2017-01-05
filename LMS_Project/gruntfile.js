/*global module */
module.exports = function (grunt) {
    'use strict';

    grunt.initConfig({
        sass: {
            options: {
                sourceMap: true
            },
            dist: {
                files: { "style.css": "sass/style.scss" }
            }
        },
        uglify: {
            options: {
                sourceMap: true
            },
            core: { files: { 'project.core.min.js': ['LMS/*.js', 'LMS/**/*.js'] } },
            angular: { files: { 'project.angular.min.js': ['Scripts/angular.js', 'Scripts/angular-route.js', 'Scripts/angular-animate.min.js', 'Scripts/angular-messages.js'] } },
            vendor: { files: { 'project.vendor.min.js': ['Scripts/jquery-3.1.1.js', 'Scripts/bootstrap.js'] } }
        },
        watch: {
            assets: { files: ['Sass/**/*.scss'], tasks: ['sass'] },
            scripts: { files: ['Scripts/**/*.js', 'LMS/**/*.js'], tasks: ['uglify'] }
        }
    });

    grunt.registerTask('watch', ['watch']);

    grunt.loadNpmTasks("grunt-contrib-watch");
    grunt.loadNpmTasks("grunt-contrib-uglify");
    grunt.loadNpmTasks("grunt-sass");
};