import { SignInCommand } from './../api/identity/models/SignInCommand';
import { IdentityService, RequestPasswordResetCommand, ResendConfirmationEmailCommand, SignInResponse, SignUpCommand, SignUpResponse } from "../api/identity"

export const signIn = async (command : SignInCommand):Promise<SignInResponse> => {
    return await IdentityService.postApiIdentitySignIn({requestBody: command});
}

export const signUp = async (command: SignUpCommand):Promise<SignUpResponse> => {
    return await IdentityService.postApiIdentitySignUp({requestBody: command});
}

export const resendEmailConfirmation = async (command: ResendConfirmationEmailCommand): Promise<void> => {
    return await IdentityService.postApiIdentityEmailResendConfirmation({requestBody: command});
}

export const requestPasswordReset = async (command: RequestPasswordResetCommand): Promise<void> => {
    return await IdentityService.postApiIdentityPasswordRequestReset({requestBody: command});
}