import React, {Component} from "react";
import api from "../../../../common/api";
import { Field } from "redux-form";
import SelectField from "../../../../components/inputs/SelectField";

class SelectProductTypes extends Component {

  
    constructor(props) {
        super(props);
        this.state = {
          // some initial state
          options: []
        }
      }

    componentDidMount(){
        // acá va el código de fetch
        // luego
    
        api.get('/productType')
        .then(response => {
        
                this.setState({
                    // some initial state
                    options: this.state.options.concat([{ value: "", label: 'Seleccione Categoría'}])
                });
                let categorias = response.data.map(each => {
                    return {
                        value : each.id,
                        label: each.description
                    }
                });
                this.setState({options:this.state.options.concat(categorias)});
        })
    }

    render() {    
    return (
        <Field label="Categorías" options={this.state.options} name="productTypeId" component={SelectField} type ="select"></Field>
    );
  }
}

export default SelectProductTypes; 