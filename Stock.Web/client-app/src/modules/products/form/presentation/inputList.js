import React, {Component} from "react";
import { Field } from "redux-form";
import api from "../../../../common/api";
import SelectField from "../../../../components/inputs/SelectField";

class InputList extends Component {
    
    constructor() {
        super();
        this._isMounted = false;
        this.state = {
            options: []
        };
    }

    componentDidMount(){
        this._isMounted = true;
        if(this.props.URL !== undefined){
            api.get(this.props.URL)
            .then(response => {
                if(this._isMounted){
                    this.setState({
                        options: this.state.options.concat([{ value: "", label: 'Seleccionar ' + this.props.label}])
                    });
                    response.data.forEach(element => {
                        this.setState({
                            options: this.state.options.concat([{
                                value: element.id,
                                label: element[this.props.param]
                            }])
                        });
                    });
                }
            })
            .catch(() => {
                return null;
            });
        }   
    }

    componentWillUnmount() {
        this._isMounted = false;
    }

    render() {    
        return (
            <Field 
                label={this.props.label} 
                options={this.state.options} 
                name={this.props.name} 
                component={SelectField}> 
            </Field>
        );
    }
}

export default InputList;