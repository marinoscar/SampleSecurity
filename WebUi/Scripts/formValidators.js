formValidators = {
    userRegister: function () {
        $('#registerForm').bootstrapValidator({
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            fields: {
                UserName: {
                    validators: {
                        notEmpty: {
                            message: 'The email address is required'
                        },
                        emailAddress: {
                            message: 'The email address is not valid'
                        }
                    }
                },
                Name: {
                    validators: {
                        notEmpty: {
                            message: 'The name is required'
                        }
                    }
                },
                Website: {
                    validators: {
                        uri: {
                            message: 'The web site address is invalid'
                        }
                    }
                },
            }
        });
    },
    userManage: function () {
        $('#loginForm').bootstrapValidator({
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            fields: {
                UserName: {
                    validators: {
                        notEmpty: {
                            message: 'The email is required'
                        },
                        emailAddress: {
                            message: 'The email address is not valid'
                        }
                    }
                },
                UserPassword: {
                    validators: {
                        notEmpty: {
                            message: 'The password is required'
                        }
                    }
                }
            }
        });
    }
    userLogin: function () {
        $('#loginForm').bootstrapValidator({
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            fields: {
                UserName: {
                    validators: {
                        notEmpty: {
                            message: 'The email is required'
                        },
                        emailAddress: {
                            message: 'The email address is not valid'
                        }
                    }
                },
                UserPassword: {
                    validators: {
                        notEmpty: {
                            message: 'The password is required'
                        }
                    }
                }
            }
        });
    }
}