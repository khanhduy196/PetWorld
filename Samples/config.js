//all the provided keys are examples, go to Amazon Cognito and get yours

AWSCognito.config.region = 'eu-east-1'; //This is required to derive the endpoint

var poolData = {
    UserPoolId: 'us-east-1_EttBoTOfx', // your user pool id here
    ClientId: '4s8cbl7ptf75hp2tbqc14coib7' // your client id here
};

var identityPoolId = 'us-east-1:034148949310:userpool/us-east-1_EttBoTOfx'; //go to AWS Cognito Federated Identites

var userAttributes = [{
    'Name': 'Phone Number',
    'Value': 'phone_number'
}, {
    'Name': 'Name',
    'Value': 'name'
}]; //the standard attributes you require in AWS Cognito

var MFARequired = true; //do you require your clients to use MFA?