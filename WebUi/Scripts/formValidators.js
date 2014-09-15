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
                UserPassword: {
                    validators: {
                        notEmpty: {
                            message: 'The password is required'
                        },
                        identical: {
                            field: 'ConfirmUserPassword',
                            message: 'The password does not match'
                        }
                    }
                },
                ConfirmUserPassword: {
                    validators: {
                        notEmpty: {
                            message: 'The password confirmation is required'
                        },
                        identical: {
                            field: 'UserPassword',
                            message: 'The password does not match'
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
            }
        });
    },
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